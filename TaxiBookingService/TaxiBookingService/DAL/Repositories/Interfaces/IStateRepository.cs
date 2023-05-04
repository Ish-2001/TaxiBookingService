using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IStateRepository : IGenericRepository<State>
    {
        public string GetStateName(int stateId);
    }
}
