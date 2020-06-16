using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{
  /// <summary>
  /// Represents the _Booking_ context
  /// </summary>
  public class BookingContext : DbContext
  {
    public DbSet<BookingModel> Bookings { get; set; }
    public DbSet<StayModel> Stays { get; set; }

    public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<BookingModel>().Property(e => e.Id).HasIdentityOptions(startValue: 100);

      modelBuilder.Entity<GuestModel>().HasKey(e => e.Id);

      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);

      modelBuilder.Entity<StayModel>().HasKey(e => e.Id);
      modelBuilder.Entity<StayModel>().Property(e => e.DateCreated).ValueGeneratedOnAdd();
      modelBuilder.Entity<StayModel>().Property(e => e.DateModified).ValueGeneratedOnAdd().ValueGeneratedOnUpdate();

      // * Seed Data
      modelBuilder.Entity<BookingModel>().HasData(new BookingModel { Id = 1, AccountId = 1, LodgingId = 1, Guests = null, Rentals = null, Status = "Booked", Stay = null });
      modelBuilder.Entity<BookingModel>().HasData(new BookingModel { Id = 2, AccountId = 2, LodgingId = 2, Guests = null, Rentals = null, Status = "Cancelled", Stay = null });
      modelBuilder.Entity<BookingModel>().HasData(new BookingModel { Id = 3, AccountId = 3, LodgingId = 3, Guests = null, Rentals = null, Status = "Occupied", Stay = null });
    }
  }
}
