using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWorkDriver _unitOfWork;
        private readonly IUserService _userService;
        private readonly IVehicleDetailsService _vehicleDetailsService;
        private readonly IHashingService _hashingService;
        private readonly IRideCancellationService _rideCancellationService;

        public DriverService(IUnitOfWorkDriver unitOfWork, IUserService userService, IHashingService hashingService, IVehicleDetailsService vehicleDetailsService, IRideCancellationService rideCancellationService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _hashingService = hashingService;
            _vehicleDetailsService = vehicleDetailsService;
            _rideCancellationService = rideCancellationService;
        }

        public List<Driver> GetAll()
        {
            return _unitOfWork.Drivers.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayDriverDTO> GetDrivers()
        {
            List<DisplayDriverDTO> drivers = new List<DisplayDriverDTO>();

            List<Driver> existingDrivers = _unitOfWork.Drivers.GetAll(item => item.IsDeleted == false);

            foreach (var driver in existingDrivers)
            {
                User user = _unitOfWork.Users.Get(item => item.Id == driver.UserId);

                DisplayDriverDTO newdisplayDriver = new()
                {
                    LicenseNumber = driver.LicenseNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    Rating = driver.Rating 
                };

                drivers.Add(newdisplayDriver);
            }

            return drivers;
        }

        public bool Add(DriverDTO driverDTO)
        {
            UserRole userRole = _unitOfWork.UserRoles.Get(item => item.Role == driverDTO.Role && item.IsDeleted == false);
            bool userExists = _unitOfWork.Users.Exists(item => item.UserName == driverDTO.UserName && item.IsDeleted == false);

            if (userExists) return false;

            if (userRole == null) return false;

            User user = _userService.AddUser(driverDTO, userRole);

            Driver driver = new()
            {
                LicenseNumber = driverDTO.LicenseNumber,
                IsActive = false,
                UserId = user.Id,
                //LocationId = 0,
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id
            };


            _unitOfWork.Drivers.Add(driver);
            _unitOfWork.Complete();

            return true;
        }

        
        public bool Login(LoginDTO login)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == login.UserName);

            if (user == null) return false;

            string role = _unitOfWork.UserRoles.GetRoleName(user.RoleId);

            if (!(role == Role.Driver.ToString())) return false;

            bool isValidPassword = _hashingService.VerifyHashing(login.Password, user.Password);

            if (!isValidPassword)
                return false;

            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id);

            driver.IsActive = true;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Complete();

            return true;
        }

        public bool CompletePayment(int id , PaymentDTO updatedPayment)
        {
            Booking booking = _unitOfWork.Bookings.Get(item => item.Id == id);
            Payment payment = _unitOfWork.Payments.Get(item => item.BookingId == booking.Id);
            PaymentMode paymentMode = _unitOfWork.PaymentModes.Get(item => item.Id == booking.ModeId);
            User user = _unitOfWork.Users.Get(item => item.Id == booking.UserId);
            Driver driver = _unitOfWork.Drivers.Get(item => item.Id == booking.DriverId);

            if (booking == null) return false;

            payment.IsPending = false;

            if(paymentMode.Mode == PaymentModes.Wallet.ToString())
            {
                user.Balance -= payment.Amount;
                _unitOfWork.Users.Update(user);
                _unitOfWork.Complete();
                user = _unitOfWork.Users.Get(item => item.Id == driver.UserId);
                user.Balance += payment.Amount;
                _unitOfWork.Users.Update(user);
            }

            _rideCancellationService.PayCancellationFee(user.UserName);

            _unitOfWork.Payments.Update(payment);
          
            _unitOfWork.Complete();

            return true;
        }
    }
}
