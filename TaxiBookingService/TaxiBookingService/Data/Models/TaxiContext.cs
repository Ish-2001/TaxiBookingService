
//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
//using System.Data.Entity;

namespace TaxiBookingService.Data.Models
{
    public class TaxiContext : DbContext
    {
        public TaxiContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(e => e.UserRole).
                       WithMany(r => r.Users).
                       HasForeignKey(m => m.RoleId);
            });

        }
        DbSet<User> Users { get; set; }
        DbSet<Driver> Drivers { get; set; } 
        DbSet<Location> Locations { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<State> States { get; set; }   
        DbSet<Area> Areas { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<PaymentMode> PaymentsMode { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<BookingStatus> BookingsStatus { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<CancellationReason> CancellationReasons { get; set; } 
        DbSet<VehicleCategory> VehicleCategories { get; set; }
        DbSet<VehicleDetails> VehicleDetails { get; set; }  
        DbSet<RideCancellation> RideCancellation { get; set; }
        DbSet<DriverRating> DriverRating { get; set; }
        DbSet<DriverComplaint> DriverComplaints { get; set; }
    }
}
