using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Booking.ObjectModel.Models
{
    /// <summary>
    /// Represents the _Stay_ model
    /// </summary>
    public class StayModel : IValidatableObject
    {
        /// <summary>
        /// Stay's unique Id
        /// </summary>
        /// <value></value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The Check In date for the Stay
        /// </summary>
        /// <value></value>
        [Column(TypeName = "date")]
        [Required]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// The Check Out date for the Stay
        /// </summary>
        /// <value></value>
        [Column(TypeName = "date")]
        [Required]
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// Timestamp when the Stay was created
        /// </summary>
        /// <value></value>
        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///  Timestamp for when the Stay was last modified
        /// </summary>
        /// <value></value>
        [Column(TypeName = "date")]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Foreign key relation for the Booking Model
        /// </summary>
        /// <value></value>
        [ForeignKey("Booking")]
        public int? BookingId { get; set; }
        public virtual BookingModel Booking { get; set; }

        public StayModel()
        {
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        /// <summary>
        /// Represents the _Stay_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
