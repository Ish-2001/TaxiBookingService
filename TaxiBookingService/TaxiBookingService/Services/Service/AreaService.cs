using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class AreaService : IAreaService
    {
        private readonly IUnitOfWorkArea _unitOfWork;

        public AreaService(IUnitOfWorkArea unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Area> GetAll()
        {
            return _unitOfWork.Areas.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayAreaDTO> GetAreas()
        {
            List<DisplayAreaDTO> areas = new List<DisplayAreaDTO>();

            List<Area> existingAreas = _unitOfWork.Areas.GetAll();

            foreach (var area in existingAreas)
            {
                DisplayAreaDTO newdisplayArea = new()
                {
                    Name = area.Name,
                    Locality = area.Locality,
                    City = area.City.Name
                };

                areas.Add(newdisplayArea);
            }

            return areas;
        }

        public bool Add(AreaDTO newArea)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newArea.UserName);
            bool areaExists = _unitOfWork.Areas.Exists(item => item.Name == newArea.Name && item.IsDeleted == false);
            City city = _unitOfWork.Cities.Get(item => item.Name == newArea.City);        

            if (user == null) return false;

            if (city == null) return false;

            if (areaExists) return false;
          

            Area area = new()
            {
                Name = newArea.Name,
                Locality = newArea.Locality,
                CityId = city.Id,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.Areas.Add(area);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(AreaDTO updatedArea, int id)
        {
            Area area = _unitOfWork.Areas.Get(item => item.Id == id);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedArea.UserName);

            if (area == null) return false;

            if (area.IsDeleted) return false;

            area.Name = updatedArea.Name;
            area.Locality = updatedArea.Locality;
            area.ModifiedAt = DateTime.Now;
            area.ModifiedBy = user.Id;

            _unitOfWork.Areas.Update(area);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            Area area = _unitOfWork.Areas.Get(item => item.Id == id);

            if (area == null) return false;
            if (area.IsDeleted) return false;

            area.IsDeleted = true;

            _unitOfWork.Areas.Update(area);
            _unitOfWork.Complete();

            return true;
        }

    }
}
