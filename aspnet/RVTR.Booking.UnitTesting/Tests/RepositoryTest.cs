using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
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

        public static readonly IEnumerable<object[]> _bothRecords = new List<object[]>()
    {

      new object[]
      {
        new BookingModel()
        {
          Id = 1,
          AccountId = 1,
          LodgingId = 1,
          Guests = new List<GuestModel>(),
          BookingRentals = new List<BookingRentalModel>(),
          Status = "status",
          Stay = new StayModel()
          {
            Id = 1,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now,
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
            BookingId = 1
          }
        },
        new StayModel()
        {
          Id = 2,
          CheckIn = DateTime.Now,
          CheckOut = DateTime.Now,
          DateCreated = DateTime.Now,
          DateModified = DateTime.Now,
          BookingId = 1
        }
      }
    };

        public static readonly IEnumerable<object[]> _bookingOnlyRecords = new List<object[]>()
    {
      new object[]
      {
        new BookingModel()
        {
          Id = 0,
          AccountId = 0,
          LodgingId = 0,
          Guests = new List<GuestModel>(),
          BookingRentals = new List<BookingRentalModel>(),
          Status = "status",
          Stay = new StayModel()
        }
      }
    };

        [Theory]
        [MemberData(nameof(_bookingOnlyRecords))]
        public async void Test_Repository_DeleteAsync(BookingModel booking)
        {
            await _connection.OpenAsync();

            try
            {
                using (var ctx = new BookingContext(_options))
                {
                    await ctx.Database.EnsureCreatedAsync();
                    await ctx.Bookings.AddAsync(booking);
                    await ctx.SaveChangesAsync();
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new Repository<BookingModel>(ctx);

                    await bookings.DeleteAsync(1);
                    await ctx.SaveChangesAsync();

                    Assert.Empty(await ctx.Bookings.ToListAsync());
                }

            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bookingOnlyRecords))]
        public async void Test_Repository_InsertAsync(BookingModel booking)
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


            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bookingOnlyRecords))]
        public async void Test_BookingRepository_InsertAsync(BookingModel booking)
        {
            await _connection.OpenAsync();
            var bookingRentals = new List<BookingRentalModel>();
            bookingRentals.Add(new BookingRentalModel { RentalId = 1 });
            booking.BookingRentals = bookingRentals;
            try
            {
                using (var ctx = new BookingContext(_options))
                {
                    await ctx.Database.EnsureCreatedAsync();
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new BookingRepository(ctx);

                    await bookings.InsertAsync(booking);
                    await ctx.SaveChangesAsync();

                    Assert.NotEmpty(await ctx.Bookings.ToListAsync());
                }

            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bookingOnlyRecords))]
        public async void Test_BookingRepository_Update(BookingModel booking)
        {
            await _connection.OpenAsync();
            var bookingRentals = new List<BookingRentalModel>();
            bookingRentals.Add(new BookingRentalModel { RentalId = 1 });
            bookingRentals.Add(new BookingRentalModel { RentalId = 2 });
            booking.BookingRentals = bookingRentals;
            try
            {
                using (var ctx = new BookingContext(_options))
                {
                    await ctx.Database.EnsureCreatedAsync();
                    var bookings = new BookingRepository(ctx);
                    await bookings.InsertAsync(booking);
                    await ctx.SaveChangesAsync();
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new BookingRepository(ctx);
                    bookingRentals.RemoveAt(0);
                    bookingRentals.Add(new BookingRentalModel { RentalId = 3 });
                    booking.BookingRentals = bookingRentals;
                    bookings.Update(booking);
                    await ctx.SaveChangesAsync();
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new BookingRepository(ctx);
                    var actual = await bookings.SelectAsync(1);
                    Assert.Equal(2, actual.BookingRentals.Count());
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bookingOnlyRecords))]
        public async void Test_Repository_SelectAsync(BookingModel booking)
        {
            await _connection.OpenAsync();

            try
            {
                using (var ctx = new BookingContext(_options))
                {
                    await ctx.Database.EnsureCreatedAsync();
                    await ctx.Bookings.AddAsync(booking);
                    await ctx.SaveChangesAsync();
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new Repository<BookingModel>(ctx);

                    var actual = await bookings.SelectAsync();

                    Assert.NotEmpty(actual);
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new BookingRepository(ctx);

                    var actual = await bookings.SelectAsync();

                    Assert.NotEmpty(actual);
                }

                using (var ctx = new BookingContext(_options))
                {
                    var stays = new StayRepository(ctx);

                    var actual = await stays.SelectAsync();

                    Assert.NotEmpty(actual);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bookingOnlyRecords))]
        public async void Test_Repository_SelectAsync_ById(BookingModel booking)
        {
            await _connection.OpenAsync();

            try
            {
                using (var ctx = new BookingContext(_options))
                {
                    await ctx.Database.EnsureCreatedAsync();
                    await ctx.Bookings.AddAsync(booking);
                    // await ctx.Stays.AddAsync(stay);
                    await ctx.SaveChangesAsync();
                }

                using (var ctx = new BookingContext(_options))
                {
                    var bookings = new Repository<BookingModel>(ctx);

                    var actual = await bookings.SelectAsync(1);

                    Assert.NotNull(actual);
                }

                using (var ctx = new BookingContext(_options))
                {
                    var stays = new Repository<StayModel>(ctx);

                    // var actual = await stays.SelectAsync(2);
                    var actual = await stays.SelectAsync(1);

                    Assert.NotNull(actual);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bothRecords))]
        public async void Test_BookingandStayRepository_SelectAsync_ById(BookingModel booking, StayModel stay)
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
                    var bookings = new BookingRepository(ctx);

                    var actual = await bookings.SelectAsync(1);

                    Assert.NotNull(actual);
                }

                using (var ctx = new BookingContext(_options))
                {
                    var stays = new StayRepository(ctx);

                    var actual = await stays.SelectAsync(2);

                    Assert.NotNull(actual);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bothRecords))]
        public async void Test_Repository_selectAsync_ByFilter(BookingModel booking, StayModel stay)
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

                    var actual = await bookings.SelectAsync(null, null, "", 0, 50);

                    Assert.NotEmpty(actual);

                    actual = await bookings.SelectAsync(null, null, "", 0, 50);
                    Assert.NotEmpty(actual);
                }

                using (var ctx = new BookingContext(_options))
                {
                    var stays = new Repository<StayModel>(ctx);

                    var actual = await stays.SelectAsync(null, null, "", 0, 50);

                    Assert.NotEmpty(actual);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Theory]
        [MemberData(nameof(_bothRecords))]
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
                    var bookingRentals = new List<BookingRentalModel>();
                    bookingRentals.Add(new BookingRentalModel { RentalId = 1 });
                    booking.BookingRentals = bookingRentals;
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
