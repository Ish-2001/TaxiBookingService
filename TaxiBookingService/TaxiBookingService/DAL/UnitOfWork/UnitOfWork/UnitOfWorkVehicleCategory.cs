using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkVehicleCategory : IUnitOfWorkVehicleCategory
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkVehicleCategory(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            VehicleCategories = new VehicleCategoryRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IVehicleCategoryRepository VehicleCategories { get; }
        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
