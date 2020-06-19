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

        public override async Task InsertAsync(BookingModel booking)
        {
            foreach (var rental in booking.Rentals)
            {
                var rentalUnit = await _context.Set<RentalUnitModel>().FirstOrDefaultAsync(ru => ru.Id == rental.RentalUnit.Id);
                if (rentalUnit != null)
                    rental.RentalUnit = rentalUnit;
            }

            await _db.AddAsync(booking).ConfigureAwait(true);
        }

        public override async Task<IEnumerable<BookingModel>> SelectAsync() => await IncludeQuery().ToListAsync();

        public override async Task<BookingModel> SelectAsync(int id) => await IncludeQuery().FirstAsync(booking => booking.Id == id);

        private IQueryable<BookingModel> IncludeQuery()
            => _db.Include("Guests")
            .Include("Rentals")
            .Include("Rentals.RentalUnit");
    }
}
