using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Booking.ObjectModel.Models
{
  public class RentalModel : IValidatableObject
  {
    [Key]
    [Required]
    public int Id { get; set; }

    public IEnumerable<BookingRentalModel> BookingRentals { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
