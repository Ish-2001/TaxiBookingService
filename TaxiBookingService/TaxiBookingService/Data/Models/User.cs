using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBookingService.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string UserName { get; set; }

        [Required]      
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string PhoneNumber { get; set; }

        [Required]
        public double Balance { get; set; }

        //[Required]
        public int RoleId { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        //[Required]
        public int? CreatedBy { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set;}

        public int? ModifiedBy { get; set;}

        [ForeignKey(nameof(CreatedBy))]
        public virtual User Created { get; set;}

        [ForeignKey(nameof(ModifiedBy))]
        public virtual User Modified { get; set;}

        [ForeignKey("RoleId")]
        public virtual UserRole UserRole { get; set;}
    }
}
