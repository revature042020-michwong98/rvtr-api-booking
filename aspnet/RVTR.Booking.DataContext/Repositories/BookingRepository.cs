using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
    public class BookingRepository : Repository<BookingModel>
    {
        public BookingRepository(BookingContext context) : base(context) { }

        // public override async Task DeleteAsync(int id) => _db.Remove(await IncludeQuery().Include("Stay").FirstAsync(booking => booking.Id == id));

        public override async Task<IEnumerable<BookingModel>> SelectAsync() => await IncludeQuery().ToListAsync();

        public override async Task<BookingModel> SelectAsync(int id) => await IncludeQuery().FirstAsync(booking => booking.Id == id);

        private IQueryable<BookingModel> IncludeQuery()
            => _db.Include("Guests")
            .Include("Rentals");
    }
}
