using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Booking.ObjectModel.Models
{
    public class BookingRentalModel : IValidatableObject
  {
    public int BookingId { get; set; }
    public BookingModel Booking { get; set; }

    public int RentalId { get; set; }
    public RentalModel Rental { get; set; }

    /// <summary>
    /// Represents the _Booking_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}