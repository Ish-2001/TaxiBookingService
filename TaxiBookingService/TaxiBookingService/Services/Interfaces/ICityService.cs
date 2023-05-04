using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface ICityService
    {
        List<City> GetAll();
        List<DisplayCityDTO> GetCities();
        bool Add(CityDTO newCity);
        bool Update(CityDTO updatedCity, int id);
        bool Delete(int id);
    }
}
