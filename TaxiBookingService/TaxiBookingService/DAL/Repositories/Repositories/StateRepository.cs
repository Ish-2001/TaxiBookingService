using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(TaxiContext _context) : base(_context)
        {

        }

        public string GetStateName(int stateId)
        {
            return FindAll(item => item.Id == stateId).Select(item => item.Name).FirstOrDefault();
        }
    }
}
