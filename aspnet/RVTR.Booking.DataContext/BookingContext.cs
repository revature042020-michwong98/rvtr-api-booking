using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{
  /// <summary>
  /// Database context used to query and insert entities to the Db.
  /// </summary>
  public class BookingContext : DbContext
  {
    /// <summary>
    /// Set used to query and insert instances of the `BookingModel` entity
    /// </summary>
    /// <value></value>
    public DbSet<BookingModel> Bookings { get; set; }
    /// <summary>
    /// Set used to query and insert instances of the `BookingModel` entity
    /// </summary>
    /// <value></value>
    public DbSet<StayModel> Stays { get; set; }

    /// <summary>
    /// Database context used to query and insert entities to the Db.
    /// </summary>
    /// <param name="options">Options to provide to context</param>
    /// <returns></returns>
    public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<BookingModel>().HasMany(b => b.Guests).WithOne(g => g.Booking).IsRequired().OnDelete(DeleteBehavior.Cascade);
      modelBuilder.Entity<BookingModel>().HasOne(b => b.Stay).WithOne(s => s.Booking).IsRequired().OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<GuestModel>().HasKey(e => e.Id);

      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);

      modelBuilder.Entity<StayModel>().HasKey(e => e.Id);
      modelBuilder.Entity<StayModel>().Property(e => e.DateCreated).ValueGeneratedOnAdd();
      modelBuilder.Entity<StayModel>().Property(e => e.DateModified).ValueGeneratedOnAdd().ValueGeneratedOnUpdate();

      // * Seed Data
      // modelBuilder.Entity<BookingModel>().HasData(new BookingModel { Id = 1, AccountId = 1, LodgingId = 1, Guests = null, Rentals = null, Status = "Booked", Stay = null });
      // modelBuilder.Entity<BookingModel>().HasData(new BookingModel { Id = 2, AccountId = 2, LodgingId = 2, Guests = null, Rentals = null, Status = "Cancelled", Stay = null });
      // modelBuilder.Entity<BookingModel>().HasData(new BookingModel { Id = 3, AccountId = 3, LodgingId = 3, Guests = null, Rentals = null, Status = "Occupied", Stay = null });
    }
  }
}
