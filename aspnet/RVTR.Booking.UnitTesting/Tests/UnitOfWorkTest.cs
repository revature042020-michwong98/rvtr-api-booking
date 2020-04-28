using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class UnitOfWorkTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;
    private static readonly BookingContext _context = new BookingContext(_options);
    public static readonly IEnumerable<object[]> _unitOfWorks = new List<object[]>
    {
      new object[] { new UnitOfWork(_context) }
    };

    [Theory]
    [MemberData(nameof(_unitOfWorks))]
    public async void Test_UnitOfWork_CommitAsync(UnitOfWork unitOfWork)
    {
      var actual = await unitOfWork.CommitAsync();

      Assert.True(actual >= 0);
    }
  }
}
