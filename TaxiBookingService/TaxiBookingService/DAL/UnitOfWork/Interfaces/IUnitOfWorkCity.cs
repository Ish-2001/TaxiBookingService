using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkCity
    {
        void Complete();

        public IUserRepository Users { get; }
        public ICityRepository Cities { get; }
        public IStateRepository States { get; }


    }
}
