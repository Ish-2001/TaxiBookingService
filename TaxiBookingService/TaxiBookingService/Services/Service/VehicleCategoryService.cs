using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class VehicleCategoryService : IVehicleCategoryService
    {
        private readonly IUnitOfWorkVehicleCategory _unitOfWork;

        public VehicleCategoryService(IUnitOfWorkVehicleCategory unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<VehicleCategory> GetAll()
        {
            return _unitOfWork.VehicleCategories.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayVehicleCategoryDTO> GetVehicleCategories()
        {
            List<DisplayVehicleCategoryDTO> categories = new List<DisplayVehicleCategoryDTO>();

            List<VehicleCategory> existingCategories = _unitOfWork.VehicleCategories.GetAll(item => item.IsDeleted == false);

            foreach (var status in existingCategories)
            {
                DisplayVehicleCategoryDTO newdisplayCategory = new()
                {
                    Type = status.Type,
                };

                categories.Add(newdisplayCategory);
            }

            return categories;
        }

        public bool Add(VehicleCategoryDTO newCategory)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newCategory.UserName && item.IsDeleted == false);
            bool categoryExists = _unitOfWork.VehicleCategories.Exists(item => item.Type == newCategory.Type && item.IsDeleted == false);

            if (user == null) return false;

            if (categoryExists) return false;

            VehicleCategory category = new()
            {
                Type = newCategory.Type,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.VehicleCategories.Add(category);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(VehicleCategoryDTO updatedCategory, int id)
        {
            VehicleCategory category = _unitOfWork.VehicleCategories.Get(item => item.Id == id && item.IsDeleted == false);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedCategory.UserName && item.IsDeleted == false);

            if (category == null) return false;

            category.Type = updatedCategory.Type;
            category.ModifiedAt = DateTime.Now;
            category.ModifiedBy = user.Id;

            _unitOfWork.VehicleCategories.Update(category);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            VehicleCategory category = _unitOfWork.VehicleCategories.Get(item => item.Id == id);

            if (category == null) return false;
            if (category.IsDeleted) return false;

            category.IsDeleted = true;

            _unitOfWork.VehicleCategories.Update(category);
            _unitOfWork.Complete();

            return true;
        }
    }
}

