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
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "date")]
    [Required]
    public DateTime CheckIn { get; set; }

    [Column(TypeName = "date")]
    [Required]
    public DateTime CheckOut { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateModified { get; set; }

    [Required]
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
