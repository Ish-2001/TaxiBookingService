using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkPaymentMode
    {
        public IUserRepository Users { get; }
        public IPaymentModeRepository PaymentModes { get; }

        public void Complete();
    }
}
