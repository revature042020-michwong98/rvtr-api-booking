using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;


namespace RVTR.Booking.DataContext.Repositories
{
  /// <summary>
  /// Repository class for the `StayModel`.  Utilizes CRUD operations.
  /// </summary>
  public class StayRepository : Repository<StayModel>
  {
    /// <summary>
    /// Repository class for the `StayModel`.  Utilizes CRUD operations.
    /// </summary>
    /// <param name="context">Db context used to perform operations against the Db</param>
    /// <returns></returns>
    public StayRepository(BookingContext context) : base(context) { }

    /// <summary>
    /// Queries the DB to fetch all `StayModel` records
    /// </summary>
    /// <returns>A `Task` that awaits for a list of `StayModel` objects from the DbContext</returns>
    public override async Task<IEnumerable<StayModel>> SelectAsync()
    {
      return await _db.ToListAsync();
    }
  }
}
