using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkVehicleCategory
    {
        public IUserRepository Users { get; }
        public IVehicleCategoryRepository VehicleCategories { get; }
        public void Complete();
    }
}
