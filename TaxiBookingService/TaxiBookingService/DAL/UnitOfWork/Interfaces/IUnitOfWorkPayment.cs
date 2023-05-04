using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkPayment
    {
        public IUserRepository Users { get; }
        public IPaymentRepository Payments { get; }

        public void Complete();
    }
}
