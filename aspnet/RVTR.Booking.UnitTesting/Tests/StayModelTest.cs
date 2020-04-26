using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class StayModelTest
  {
    public static readonly IEnumerable<Object[]> _stays = new List<Object[]>
    {
      new object[]
      {
        new StayModel()
        {
          Id = 0,
          CheckIn = DateTime.Now,
          CheckOut = DateTime.Now,
          DateCreated = DateTime.Now,
          DateModified = DateTime.Now
        }
      }
    };

    [Theory]
    [MemberData(nameof(_stays))]
    public void Test_Create_StayModel(StayModel stay)
    {
      var validationContext = new ValidationContext(stay);
      var actual = Validator.TryValidateObject(stay, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_stays))]
    public void Test_Validate_StayModel(StayModel stay)
    {
      var validationContext = new ValidationContext(stay);

      Assert.Empty(stay.Validate(validationContext));
    }
  }
}
