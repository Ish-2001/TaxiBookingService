﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string Role { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        //[Required]
        //[ForeignKey("User")]
        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }

        //[ForeignKey("User")]
        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        public virtual User Modified { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User Created { get; set; }

        public ICollection<User> Users { get; set; }


    }
}
