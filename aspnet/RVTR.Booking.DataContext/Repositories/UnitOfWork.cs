using System.Threading.Tasks;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork
  {
    private readonly BookingContext _context;

    public virtual Repository<BookingModel> Booking { get; }
    public virtual Repository<StayModel> Stay { get; }

    public UnitOfWork(BookingContext context)
    {
      _context = context;

      Booking = new Repository<BookingModel>(context);
      Stay = new Repository<StayModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
