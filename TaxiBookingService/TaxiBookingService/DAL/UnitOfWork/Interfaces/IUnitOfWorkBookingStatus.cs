using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkBookingStatus
    {
        IBookingStatusRepository BookingsStatus { get; }
        IUserRepository Users { get; }

        void Complete();
    }
}
