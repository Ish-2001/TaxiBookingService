using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkPaymentMode : IUnitOfWorkPaymentMode
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkPaymentMode(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            PaymentModes = new PaymentModeRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IPaymentModeRepository PaymentModes { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
