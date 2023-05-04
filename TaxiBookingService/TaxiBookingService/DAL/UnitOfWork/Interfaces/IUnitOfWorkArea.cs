using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkArea
    {
        void Complete();

        public IUserRepository Users { get; }
        public IAreaRepository Areas { get; }
        public ICityRepository Cities { get; }

    }
}
