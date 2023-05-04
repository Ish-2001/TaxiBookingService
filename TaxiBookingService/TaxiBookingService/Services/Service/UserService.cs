using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using System.Globalization;
using NLog.Fluent;
using TaxiBookingService.Services.Interfaces;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkUser _unitOfWork;
        private readonly IHashingService _hashingService;

        public UserService(IUnitOfWorkUser unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public List<User> GetAll()
        {
            return _unitOfWork.Users.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayUserDTO> GetUsers()
        {
            List<DisplayUserDTO> users = new List<DisplayUserDTO>();

            List<User> existingUsers = _unitOfWork.Users.GetAll();

            foreach (var user in existingUsers)
            {
                DisplayUserDTO newdisplayUser = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender
                };

                users.Add(newdisplayUser);
            }

            return users;
        }

        public User AddUser(DriverDTO newUser, UserRole userRole)
        {

            User user = new()
            {
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Gender = newUser.Gender,
                PhoneNumber = newUser.PhoneNumber,
                Password = _hashingService.Hashing(newUser.Password),
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                RoleId = userRole.Id
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();

            return user;
        }

        public bool Add(UserDTO newUser)
        {
            UserRole userRole = _unitOfWork.UserRoles.Get(item => item.Role == newUser.Role && item.IsDeleted == false);
            bool userExists = _unitOfWork.Users.Exists(item => item.UserName == newUser.UserName && item.IsDeleted == false);

            if (userExists) return false;

            if (userRole == null) return false;

            User user = new()
            {
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Gender = newUser.Gender,
                PhoneNumber = newUser.PhoneNumber,
                Password = _hashingService.Hashing(newUser.Password),
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                RoleId = userRole.Id
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(UpdateUserDTO updatedAsset, int id)
        {
            User user = _unitOfWork.Users.Get(item => item.Id == id && item.IsDeleted == false);
            UserRole userRole = _unitOfWork.UserRoles.Get(item => item.Role == updatedAsset.Role && item.IsDeleted == false);

            if (user == null) return false;

            if (userRole == null) return false;

            user.Email = updatedAsset.Email;
            user.Gender = updatedAsset.Gender;
            user.FirstName = updatedAsset.FirstName;
            user.LastName = updatedAsset.LastName;
            user.PhoneNumber = updatedAsset.PhoneNumber;
            user.UserName = updatedAsset.UserName;
            user.Balance = updatedAsset.Balance;
            user.ModifiedAt = DateTime.Now;
            user.ModifiedBy = id;
            user.RoleId = userRole.Id;

            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            User user = _unitOfWork.Users.Get(item => item.Id == id);

            if (user == null) return false;
            if (user.IsDeleted) return false;

            user.IsDeleted = true;

            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();

            return true;
        }

        public bool Login(LoginDTO login)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == login.UserName);

            if (user == null) return false;

            string role = _unitOfWork.UserRoles.GetRoleName(user.RoleId);

            if (!(role == Role.User.ToString())) return false;

            bool isValidPassword = _hashingService.VerifyHashing(login.Password, user.Password);

            if (!isValidPassword)
                return false;

            return true;
        }
    }
}
