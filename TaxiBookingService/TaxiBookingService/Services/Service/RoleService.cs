using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWorkUserRole _unitOfWork;

        public RoleService(IUnitOfWorkUserRole unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<UserRole> GetAll()
        {
            return _unitOfWork.UserRoles.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayRolesDTO> GetRoles()
        {
            List<DisplayRolesDTO> roles = new List<DisplayRolesDTO>();

            List<UserRole> existingRoles = _unitOfWork.UserRoles.GetAll(item => item.IsDeleted == false);

            foreach (var status in existingRoles)
            {
                DisplayRolesDTO newdisplayRole = new()
                {
                    Role = status.Role,
                };

                roles.Add(newdisplayRole);
            }

            return roles;
        }

        public bool Add(UserRoleDTO newRole)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newRole.UserName && item.IsDeleted == false);
            bool roleExists = _unitOfWork.UserRoles.Exists(item => item.Role == newRole.Role && item.IsDeleted == false);

            if (user == null) return false;

            if (roleExists) return false;

            UserRole role = new()
            {
                Role = newRole.Role,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.UserRoles.Add(role);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(UserRoleDTO updatedRole, int id)
        {
            UserRole role = _unitOfWork.UserRoles.Get(item => item.Id == id);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedRole.UserName);

            if (role == null) return false;

            role.Role = updatedRole.Role;
            role.ModifiedAt = DateTime.Now;
            role.ModifiedBy = user.Id;

            _unitOfWork.UserRoles.Update(role);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            UserRole role = _unitOfWork.UserRoles.Get(item => item.Id == id);

            if (role == null) return false;
            if (role.IsDeleted) return false;

            role.IsDeleted = true;

            _unitOfWork.UserRoles.Update(role);
            _unitOfWork.Complete();

            return true;
        }
    }
}
