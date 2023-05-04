using Microsoft.IdentityModel.Protocols;
using System.Reflection.Metadata.Ecma335;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWorkBooking _unitOfWork;
        private readonly IPaymentService _paymentService;

        public BookingService(IUnitOfWorkBooking unitOfWork, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public List<Booking> GetAll()
        {
            return _unitOfWork.Bookings.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayBookingDTO> GetBookings()
        {
            List<DisplayBookingDTO> bookings = new List<DisplayBookingDTO>();

            List<Booking> existingBooking = _unitOfWork.Bookings.GetAll();

            foreach (var booking in existingBooking)
            {
                DisplayBookingDTO newdisplayBooking = new()
                {
                    PickupLocation = booking.PickupLocation,
                    Destination = booking.Destination,
                    Date = booking.Date,
                    Status = booking.Status.Status,
                    RideFare = booking.RideFare,
                    RideTime = booking.RideTime,
                    UserName = booking.User.FirstName + booking.User.LastName,
                    PaymentMode = booking.PaymentMode.Mode
                };

                bookings.Add(newdisplayBooking);
            }

            return bookings;
        }

        public bool Add(BookingDTO newBooking)
        {

            User user = _unitOfWork.Users.Get(item => item.UserName == newBooking.UserName && item.IsDeleted == false);
            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Status == Status.Requested.ToString() && item.IsDeleted == false);
            Booking existingBooking = _unitOfWork.Bookings.Get(item => item.UserId == user.Id && item.StatusId == status.Id && item.IsDeleted == false);
            PaymentMode mode = _unitOfWork.PaymentModes.Get(item => item.Mode == newBooking.PaymentMode && item.IsDeleted == false);

            if (status == null) return false;

            if (user == null) return false;

            if (existingBooking != null) return false;

            Booking booking = new()
            {
                PickupLocation = newBooking.PickupLocation,
                Destination = newBooking.Destination,
                RideFare = newBooking.RideFare,
                RideTime = newBooking.RideTime,
                Date = newBooking.Date,
                StatusId = status.Id,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id,
                ModeId = mode.Id
            };

            _unitOfWork.Bookings.Add(booking);
            _unitOfWork.Complete();

            bool isAdded = _paymentService.Add(booking);

            if (!isAdded) return false;

            return true;
        }

        public bool Update(BookingDTO updatedBooking, int id)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedBooking.UserName && item.IsDeleted == false);
            Booking booking = _unitOfWork.Bookings.Get(item => item.Id == id && item.IsDeleted == false);
            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Status == updatedBooking.Status && item.IsDeleted == false);
            Driver driver = null;

            if (booking == null) return false;

            if(updatedBooking.Status == Status.Booked.ToString() || updatedBooking.Status == "Arrived" || updatedBooking.Status == "Started" || updatedBooking.Status == Status.Completed.ToString())
            {
                booking.Date = updatedBooking.Date;
                booking.StatusId = status.Id;
                booking.ModifiedAt = DateTime.Now;
                booking.ModifiedBy = user.Id;


                _unitOfWork.Bookings.Update(booking);
                _unitOfWork.Complete();

                return true;
            }

            if (status.Status != Status.Cancelled.ToString())
            {
                driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id && item.IsDeleted == false);
                booking.DriverId = driver.Id;

                if(status.Status != Status.Completed.ToString())
                {
                    driver.IsActive = false;
                }
                else
                {
                    driver.IsActive = true;
                }
                _unitOfWork.Drivers.Update(driver);
            }

            status = _unitOfWork.BookingsStatus.Get(item => item.Id == booking.StatusId && item.IsDeleted == false);

            if (status.Status == Status.Requested.ToString() && booking.DriverId != null)
            {
                booking.DriverId = null;
            }

            booking.Date = updatedBooking.Date;
            booking.StatusId = status.Id;
            booking.ModifiedAt = DateTime.Now;
            booking.ModifiedBy = user.Id;

            
            _unitOfWork.Bookings.Update(booking);       
            _unitOfWork.Complete();

            return true;
        }

        public List<DisplayBookingDTO> GetBookingHistory(string userName)
        {
            List<DisplayBookingDTO> bookings = new List<DisplayBookingDTO>();

            User user = _unitOfWork.Users.Get(item => item.UserName == userName);

            List<Booking> existingBooking = _unitOfWork.Bookings.GetUserBookings(user.Id);

            foreach (var booking in existingBooking)
            {
                DisplayBookingDTO newdisplayBooking = new()
                {
                    PickupLocation = booking.PickupLocation,
                    Destination = booking.Destination,
                    Date = booking.Date,
                    Status = booking.Status.Status,
                    RideFare = booking.RideFare,
                    RideTime = booking.RideTime,
                    UserName = booking.User.FirstName + booking.User.LastName,
                    PaymentMode = booking.PaymentMode.Mode
                };

                bookings.Add(newdisplayBooking);
            }

            return bookings;
        }

        public bool PostRequest(int bookingId , string userName)
        {
            Booking booking = _unitOfWork.Bookings.Get(item => item.Id == bookingId);
            User user = _unitOfWork.Users.Get(item => item.UserName == userName);   
            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id);
            BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Id == booking.StatusId);

            if (booking == null) return false;

            if(user == null) return false;

            if(status.Status != Status.Requested.ToString()) return false;

            booking.DriverId = driver.Id;

            _unitOfWork.Bookings.Update(booking);
            _unitOfWork.Complete();

            return true;
        }
    }
}
