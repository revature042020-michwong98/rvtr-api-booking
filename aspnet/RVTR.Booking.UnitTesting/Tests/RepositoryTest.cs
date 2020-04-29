using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class RepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new BookingModel() { Id = 1 },
        new StayModel() { Id = 1 }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_DeleteAsync(BookingModel booking, StayModel stay)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Bookings.AddAsync(booking);
          await ctx.Stays.AddAsync(stay);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);

          await bookings.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Bookings.ToListAsync());
        }

        using (var ctx = new BookingContext(_options))
        {
          var stays = new Repository<StayModel>(ctx);

          await stays.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Stays.ToListAsync());
        }
      }
      finally
      {
        _connection.Close();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_InsertAsync(BookingModel booking, StayModel stay)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);

          await bookings.InsertAsync(booking);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Bookings.ToListAsync());
        }

        using (var ctx = new BookingContext(_options))
        {
          var stays = new Repository<StayModel>(ctx);

          await stays.InsertAsync(stay);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Stays.ToListAsync());
        }
      }
      finally
      {
        _connection.Close();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);

          var actual = await bookings.SelectAsync();

          Assert.Empty(actual);
        }

        using (var ctx = new BookingContext(_options))
        {
          var stays = new Repository<StayModel>(ctx);

          var actual = await stays.SelectAsync();

          Assert.Empty(actual);
        }
      }
      finally
      {
        _connection.Close();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);

          var actual = await bookings.SelectAsync(1);

          Assert.Null(actual);
        }

        using (var ctx = new BookingContext(_options))
        {
          var stays = new Repository<StayModel>(ctx);

          var actual = await stays.SelectAsync(1);

          Assert.Null(actual);
        }
      }
      finally
      {
        _connection.Close();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_Update(BookingModel booking, StayModel stay)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Bookings.AddAsync(booking);
          await ctx.Stays.AddAsync(stay);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);
          var expected = await ctx.Bookings.FirstAsync();

          expected.Status = "updated";
          bookings.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Bookings.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new BookingContext(_options))
        {
          var stays = new Repository<StayModel>(ctx);
          var expected = await ctx.Stays.FirstAsync();

          expected.DateModified = DateTime.Now;
          stays.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Stays.FirstAsync();

          Assert.Equal(expected, actual);
        }
      }
      finally
      {
        _connection.Close();
      }
    }
  }
}
