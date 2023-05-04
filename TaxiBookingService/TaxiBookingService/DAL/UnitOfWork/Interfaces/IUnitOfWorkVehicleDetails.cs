using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkVehicleDetails
    {
        public IUserRepository Users { get; }
        public IVehicleCategoryRepository VehicleCategories { get; }
        public IVehicleDetailsRepository VehiclesDetails { get; }
        public IUserRoleRepository UserRoles { get; }
        public IDriverRepository Drivers { get; }
        public void Complete();
    }
}
