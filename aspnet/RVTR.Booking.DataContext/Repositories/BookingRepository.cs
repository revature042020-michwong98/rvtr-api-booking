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
            var rentals = new List<RentalModel>();
            foreach (var rental in booking.Rentals)
            {
                var rentalEntity = await _context.Set<RentalModel>().FindAsync(rental.Id);
                if (rentalEntity != null)
                    rentals.Add(rentalEntity);
                else
                    rentals.Add(rental);
            }
            booking.Rentals = rentals.Distinct().ToList();
            await _db.AddAsync(booking).ConfigureAwait(true);
        }

        public override async Task<IEnumerable<BookingModel>> SelectAsync() => await IncludeQuery().ToListAsync();

        public override async Task<BookingModel> SelectAsync(int id) => await IncludeQuery().FirstAsync(booking => booking.Id == id);

        private IQueryable<BookingModel> IncludeQuery()
            => _db.Include("Guests")
            .Include("Rentals")
            .Include("Stay");

        public override void Update(BookingModel booking)
        {
            var rentals = new List<RentalModel>();
            foreach (var rental in booking.Rentals)
            {
                var rentalEntity = _context.Set<RentalModel>().Find(rental.Id);
                if (rentalEntity != null)
                    rentals.Add(rentalEntity);
                else
                    rentals.Add(_context.Set<RentalModel>().Add(rental).Entity);
            }
            booking.Rentals = rentals.Distinct().ToList();
            _db.Update(booking);
        }
    }
}
