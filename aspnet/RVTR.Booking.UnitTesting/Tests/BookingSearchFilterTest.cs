using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class BookingSearchFilterTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;
    
    //create an instance of a search filter query
    // everything is a string
    [Fact]
    public void Test_BookingSearchFilter_CreateAccountIdFilter()
    {
      var bookingSearchFilter = new BookingSearchFilter();
      bookingSearchFilter.CreateAccountIdFilter("1");
      Assert.Equal(1, bookingSearchFilter.AccountId);
      Assert.Single(bookingSearchFilter.Filters);
    }

    [Fact]
    public void Test_BookingSearchFilter_CreateLodgingIdFilter()
    {
      var bookingSearchFilter = new BookingSearchFilter();
      bookingSearchFilter.CreateLodgingIdFilter("1");
      Assert.Equal(1, bookingSearchFilter.LodgingId);
      Assert.Single(bookingSearchFilter.Filters);
    }

   [Fact]
   public void Test_BookingSearchQueries()
    {
      var bookingSearchQuery = new BookingSearchQueries()
      {
        AccountId = "1",
        Limit = "5",
        LodgingId = "2",
        Filter = "OrderBy",
        Offset = "0",
        Sort = "none"
      };
      Assert.Equal("1", bookingSearchQuery.AccountId);
      Assert.Equal("5", bookingSearchQuery.Limit);
      Assert.Equal("2", bookingSearchQuery.LodgingId);
      Assert.Equal("OrderBy", bookingSearchQuery.Filter);
      Assert.Equal("0", bookingSearchQuery.Offset);
      Assert.Equal("none", bookingSearchQuery.Sort);
    }
  }
}
