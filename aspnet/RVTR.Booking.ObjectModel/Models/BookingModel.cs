using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Booking.ObjectModel.Models
{
    /// <summary>
    /// Represents the _Booking_ model
    /// </summary>
    public class BookingModel : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public int LodgingId { get; set; }

        [Required]
        public IEnumerable<GuestModel> Guests { get; set; }

        [Required]
        public IEnumerable<BookingRentalModel> BookingRentals { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public StayModel Stay { get; set; }

        /// <summary>
        /// Represents the _Booking_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
