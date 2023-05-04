using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWorkLocation _unitOfWork;

        public LocationService(IUnitOfWorkLocation unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Location> GetAll()
        {
            return _unitOfWork.Locations.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayLocationDTO> GetLocations()
        {
            List<DisplayLocationDTO> locations = new List<DisplayLocationDTO>();

            List<Location> existingLocations = _unitOfWork.Locations.GetAll();

            foreach (var location in existingLocations)
            {

                DisplayLocationDTO newdisplayLocation = new()
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    City = location.City.Name,
                    State = location.State.Name,
                    Area = location.Area.Name
                };

                locations.Add(newdisplayLocation);
            }

            return locations;
        }

        public bool Add(LocationDTO newLocation)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newLocation.UserName && item.IsDeleted == false);
            State state = _unitOfWork.States.Get(item => item.Name == newLocation.State && item.IsDeleted == false);
            City city = _unitOfWork.Cities.Get(item => item.Name == newLocation.City && item.IsDeleted == false);
            Area area = _unitOfWork.Areas.Get(item => item.Name == newLocation.Area && item.IsDeleted == false);

            if (user == null) return false;
            if (state == null) return false;
            if (city == null) return false;
            if (area == null) return false;

            Location location = new()
            {
                Latitude = newLocation.Latitude,
                Longitude = newLocation.Longitude,
                StateId = state.Id,
                CityId = city.Id,
                AreaId = area.Id,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.Locations.Add(location);
            _unitOfWork.Complete();

            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id);
            driver.LocationId = location.Id;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(LocationDTO updatedLocation, int id)
        {
            Location location = _unitOfWork.Locations.Get(item => item.Id == id && item.IsDeleted == false);
            City city = _unitOfWork.Cities.Get(item => item.Name == updatedLocation.City && item.IsDeleted == false);
            State state = _unitOfWork.States.Get(item => item.Name == updatedLocation.State && item.IsDeleted == false);
            Area area = _unitOfWork.Areas.Get(item => item.Name == updatedLocation.Area && item.IsDeleted == false);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedLocation.UserName && item.IsDeleted == false);
            Driver driver = _unitOfWork.Drivers.Get(item => item.UserId == user.Id && item.IsDeleted == false);

            if (user == null) return false;
            if (location == null) return false;
            if (city == null) return false;
            if (state == null) return false;
            if (area == null) return false;

            //if (driver.LocationId != id) return false;

            location.Latitude = updatedLocation.Latitude;
            location.Longitude = updatedLocation.Longitude;
            location.StateId = state.Id;
            location.CityId = city.Id;
            location.AreaId = area.Id;
            location.ModifiedAt = DateTime.Now;
            location.ModifiedBy = driver.Id;

            _unitOfWork.Locations.Update(location);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            Location location = _unitOfWork.Locations.Get(item => item.Id == id);

            if (location == null) return false;
            if (location.IsDeleted) return false;

            location.IsDeleted = true;

            _unitOfWork.Locations.Delete(location);
            _unitOfWork.Complete();

            return true;
        }
    }
}
