using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Stay_ model
  /// </summary>
  public class StayModel : IValidatableObject
  {
    public int Id { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public int BookingId { get; set; }

    public BookingModel Booking { get; set; }

    /// <summary>
    /// Represents the _Stay_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
