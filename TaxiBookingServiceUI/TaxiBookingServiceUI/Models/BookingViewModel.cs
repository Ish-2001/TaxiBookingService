using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace TaxiBookingServiceUI.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        [DisplayName("Pickup Location")]
        [BindProperty]
        public string PickupLocation { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [BindProperty]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        [BindProperty]
        public string Destination { get; set; }

        [Required]
        [StringLength(50)]
        //[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }

        public int UserId { get; set; }

        [Required]
        [DisplayName("Ride Fare")]
        [BindProperty]
        public decimal RideFare { get; set; }

        [Required]
        [DisplayName("Ride Time")]
        [BindProperty]
        public decimal RideTime { get; set; }

        [Required]
        public string Status { get; set; }

        public int StatusId { get; set; }

        public int? DriverId { get; set; }

        public string Name { get; set; }

        [DisplayName("Payment Mode")]
        [BindProperty]
        public string PaymentMode { get; set; }

        public int ModeId { get; set; }

        [DisplayName("Vehicle Category")]
        [BindProperty]
        public string VehicleCategory { get; set; }
    }
}
