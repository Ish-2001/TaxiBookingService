using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkPayment : IUnitOfWorkPayment
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkPayment(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Payments = new PaymentRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IPaymentRepository Payments { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
