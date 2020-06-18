using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;


namespace RVTR.Booking.DataContext.Repositories
{
  public class StayRepository : Repository<StayModel>
  {
    public StayRepository(BookingContext context) : base(context) { }

    public override async Task<IEnumerable<StayModel>> SelectAsync()
    {
      return await _db.ToListAsync();
    }
  }
}
