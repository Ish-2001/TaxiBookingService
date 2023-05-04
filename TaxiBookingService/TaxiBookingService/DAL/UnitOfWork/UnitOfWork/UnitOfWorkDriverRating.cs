using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Service;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkDriverRating : IUnitOfWorkDriverRating
    {
        private readonly TaxiContext _dBContext;
        public UnitOfWorkDriverRating(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Bookings = new BookingRepository(_dBContext);
            Ratings = new DriverRatingRepository(_dBContext);
            Drivers = new DriverRepository(_dBContext);
            BookingsStatus = new BookingStatusRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IBookingRepository Bookings { get; }
        public IDriverRatingRepository Ratings { get; }
        public IDriverRepository Drivers { get; }
        public IBookingStatusRepository BookingsStatus { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
