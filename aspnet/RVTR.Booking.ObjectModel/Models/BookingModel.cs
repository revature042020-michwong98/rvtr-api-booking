using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Booking_ model
  /// </summary>
  public class BookingModel : IValidatableObject
  {
    public int Id { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Required]
    public int LodgingId { get; set; }

    [Required]
    public IEnumerable<int> Guests { get; set; }

    [Required]
    public IEnumerable<int> Rentals { get; set; }

    [Required]
    public string Status { get; set; }

    public StayModel Stay { get; set; }

    /// <summary>
    /// Represents the _Booking_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
