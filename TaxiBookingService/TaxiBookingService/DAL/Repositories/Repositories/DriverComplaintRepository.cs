using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq.Expressions;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class DriverComplaintRepository : GenericRepository<DriverComplaint> , IDriverComplaintRepository
    {
        public DriverComplaintRepository(TaxiContext _context) : base(_context)
        {

        }

        
    }
}
