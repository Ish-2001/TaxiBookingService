using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkVehicleDetails : IUnitOfWorkVehicleDetails
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkVehicleDetails(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            VehicleCategories = new VehicleCategoryRepository(_dBContext);
            VehiclesDetails = new VehicleDetailsRepository(_dBContext);
            UserRoles = new UserRoleRepository(_dBContext);
            Drivers = new DriverRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IVehicleCategoryRepository VehicleCategories { get; }
        public IVehicleDetailsRepository VehiclesDetails { get; }
        public IUserRoleRepository UserRoles { get; }
        public IDriverRepository Drivers { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
