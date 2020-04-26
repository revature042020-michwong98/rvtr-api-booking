using System;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class UnitOfWorkTest
  {
    [Fact]
    public void Test_CommitMethod()
    {
      var sut = new UnitOfWork();

      Action actual = () => sut.Commit();

      Assert.IsType<Action>(actual);
    }
  }
}
