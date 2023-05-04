using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWorkUser _unitOfWork;
        private readonly IHashingService _hashingService;

        public AdminService(IUnitOfWorkUser unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }
        public bool Login(LoginDTO login)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == login.UserName);

            if (user == null) return false;

            string role = _unitOfWork.UserRoles.GetRoleName(user.RoleId);

            if (!(role == Role.Admin.ToString())) return false;

            bool isValidPassword = _hashingService.VerifyHashing(login.Password, user.Password);

            if (!isValidPassword)
                return false;

            return true;
        }

        public bool ApproveDriver(string userName)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == userName);
            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id);

            if(user == null) return false;
            if (driver == null) return false;

            driver.IsApproved = true;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Complete();

            return true;
        }
    }
}
