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

    [Required]
    public DateTime CheckIn { get; set; }

    [Required]
    public DateTime CheckOut { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }

    [Required]
    public DateTime DateModified { get; set; }

    /// <summary>
    /// Represents the _Stay_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
