using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class VehicleDetailsService : IVehicleDetailsService
    {
        private readonly IUnitOfWorkVehicleDetails _unitOfWork;

        public VehicleDetailsService(IUnitOfWorkVehicleDetails unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<VehicleDetails> GetAll()
        {
            return _unitOfWork.VehiclesDetails.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayVehicleDetailsDTO> GetVehicleDetails()
        {
            List<DisplayVehicleDetailsDTO> details = new List<DisplayVehicleDetailsDTO>();

            List<VehicleDetails> existingDetails = _unitOfWork.VehiclesDetails.GetAll();

            foreach (var detail in existingDetails)
            {
                DisplayVehicleDetailsDTO newdisplayDetail = new()
                {
                    ModelNumber = detail.ModelNumber,
                    RegisteredName = detail.RegisteredName,
                    NumberOfSeats = detail.NumberOfSeats,
                    Name = detail.Name,
                    RegisteredNumber = detail.RegisteredNumber,
                    Type = detail.Vehicle.Type
                };

                details.Add(newdisplayDetail);
            }

            return details;
        }

        public bool Add(VehicleDetailsDTO newDetail)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newDetail.UserName && item.IsDeleted == false);
            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id);
            bool detailExists = _unitOfWork.VehiclesDetails.Exists(item => item.RegisteredNumber == newDetail.RegisteredNumber && item.IsDeleted == false);

            UserRole role = _unitOfWork.UserRoles.Get(item => item.Id == user.RoleId && item.IsDeleted == false);

            if (user == null) return false;

            if (detailExists) return false;

            if (role.Role != Role.Driver.ToString()) return false;

            VehicleCategory vehicleCategory = _unitOfWork.VehicleCategories.Get(item => item.Type == newDetail.Type && item.IsDeleted == false);

            if (vehicleCategory == null) return false;

            VehicleDetails detail = new()
            {
                Name = newDetail.Name,
                RegisteredNumber = newDetail.RegisteredNumber,
                ModelNumber = newDetail.ModelNumber,
                RegisteredName = newDetail.RegisteredName,
                NumberOfSeats = newDetail.NumberOfSeats,
                VehicleId = vehicleCategory.Id,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.VehiclesDetails.Add(detail);
            _unitOfWork.Complete();

            driver.VehicleDetailId = detail.Id;
            driver.VehicleCategoryId = vehicleCategory.Id;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(VehicleDetailsDTO updatedDetail, int id)
        {
            VehicleDetails detail = _unitOfWork.VehiclesDetails.Get(item => item.Id == id && item.IsDeleted == false);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedDetail.UserName && item.IsDeleted == false);

            if (detail == null) return false;

            detail.Name = updatedDetail.Name;
            detail.RegisteredNumber = updatedDetail.RegisteredNumber;
            detail.ModelNumber = updatedDetail.ModelNumber;
            detail.RegisteredName = updatedDetail.RegisteredName;
            detail.NumberOfSeats = updatedDetail.NumberOfSeats;
            detail.ModifiedAt = DateTime.Now;
            detail.ModifiedBy = user.Id;

            _unitOfWork.VehiclesDetails.Update(detail);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            VehicleDetails detail = _unitOfWork.VehiclesDetails.Get(item => item.Id == id);

            if (detail == null) return false;
            if (detail.IsDeleted) return false;

            detail.IsDeleted = true;

            _unitOfWork.VehiclesDetails.Update(detail);
            _unitOfWork.Complete();

            return true;
        }
    }
}

