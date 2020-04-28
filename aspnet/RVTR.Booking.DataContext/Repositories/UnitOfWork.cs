using System;
using System.Threading.Tasks;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
  public class UnitOfWork
  {
    private readonly BookingContext _context;

    public Repository<BookingModel> Bookings { get; set; }
    public Repository<StayModel> Stays { get; set; }

    public UnitOfWork(BookingContext context)
    {
      _context = context;
    }

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
