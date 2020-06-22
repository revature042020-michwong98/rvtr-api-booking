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
    public DbSet<BookingRentalModel> BookingRentals { get; set; }

    /// <summary>
    /// Database context used to query and insert entities to the Db.
    /// </summary>
    /// <param name="options">Options to provide to context</param>
    /// <returns></returns>
    public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookingRentalModel>().HasKey(br => new { br.BookingId, br.RentalId });
      modelBuilder.Entity<BookingRentalModel>().HasOne(br => br.Booking).WithMany(b => b.BookingRentals).HasForeignKey(br => br.BookingId);
      modelBuilder.Entity<BookingRentalModel>().HasOne(br => br.Rental).WithMany(r => r.BookingRentals).HasForeignKey(br => br.RentalId);

      modelBuilder.Entity<BookingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<BookingModel>().HasMany(b => b.Guests).WithOne(g => g.Booking).IsRequired().OnDelete(DeleteBehavior.Cascade);
      modelBuilder.Entity<BookingModel>().HasOne(b => b.Stay).WithOne(s => s.Booking).IsRequired().OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<GuestModel>().HasKey(e => e.Id);

      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);

      modelBuilder.Entity<StayModel>().HasKey(e => e.Id);
      modelBuilder.Entity<StayModel>().Property(e => e.DateCreated).ValueGeneratedOnAdd();
      modelBuilder.Entity<StayModel>().Property(e => e.DateModified).ValueGeneratedOnAdd().ValueGeneratedOnUpdate();
    }
  }
}
