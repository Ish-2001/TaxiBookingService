using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWorkCity _unitOfWork;

        public CityService(IUnitOfWorkCity unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<City> GetAll()
        {
            return _unitOfWork.Cities.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayCityDTO> GetCities()
        {
            List<DisplayCityDTO> cities = new List<DisplayCityDTO>();

            List<City> existingCities = _unitOfWork.Cities.GetAll();

            foreach (var city in existingCities)
            {
                DisplayCityDTO newdisplayArea = new()
                {
                    Name = city.Name,
                    State = city.State.Name
                };

                cities.Add(newdisplayArea);
            }

            return cities;
        }

        public bool Add(CityDTO newCity)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == newCity.UserName && item.IsDeleted == false);
            bool cityExists = _unitOfWork.Cities.Exists(item => item.Name == newCity.Name && item.IsDeleted == false);
            State state = _unitOfWork.States.Get(item => item.Name == newCity.State && item.IsDeleted == false);


            if (user == null) return false;

            if (cityExists) return false;

            if (state == null) return false;

            City city = new()
            {
                Name = newCity.Name,
                StateId = state.Id,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.Cities.Add(city);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(CityDTO updatedCity, int id)
        {
            City city = _unitOfWork.Cities.Get(item => item.Id == id && item.IsDeleted == false);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedCity.UserName && item.IsDeleted == false);

            if (city == null) return false;

            city.Name = updatedCity.Name;
            city.ModifiedAt = DateTime.Now;
            city.ModifiedBy = user.Id;

            _unitOfWork.Cities.Update(city);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            City city = _unitOfWork.Cities.Get(item => item.Id == id);

            if (city == null) return false;
            if (city.IsDeleted) return false;

            city.IsDeleted = true;

            _unitOfWork.Cities.Update(city);
            _unitOfWork.Complete();

            return true;
        }
    }
}
