using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IStateService
    {
        List<State> GetAll();
        List<DisplayStateDTO> GetStates();
        bool Add(StateDTO newState);
        bool Update(StateDTO updatedState, int id);
        bool Delete(int id);
    }
}
