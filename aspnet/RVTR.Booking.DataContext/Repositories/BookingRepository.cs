using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
    /// <summary>
    /// Repository class for the `BookingModel`.  Utilizes CRUD operations.
    /// </summary>
    public class BookingRepository : Repository<BookingModel>
    {
        /// <summary>
        /// Repository class for the `BookingModel` model.  Utilizes CRUD operations.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public BookingRepository(BookingContext context) : base(context) { }

        /// <summary>
        /// Inserts a new `BookingModel` record.
        /// </summary>
        /// <param name="booking">A `BookingModel` object expecting valid properties</param>
        /// <returns></returns>
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

        /// <summary>
        /// Queries the DB to fetch all `BookingModel` records that include related entities.
        /// </summary>
        /// <returns>A `Task` that awaits for a list of `BookingModel` objects</returns>
        public override async Task<IEnumerable<BookingModel>> SelectAsync() => await IncludeQuery().ToListAsync();

        /// <summary>
        /// Quries the DB to find a single `BookingModel` record using the specified Id.
        /// </summary>
        /// <param name="id">The `BookingModel`'s unique id</param>
        /// <returns></returns>
        public override async Task<BookingModel> SelectAsync(int id) => await IncludeQuery().FirstAsync(booking => booking.Id == id);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private IQueryable<BookingModel> IncludeQuery()
            => _db.Include("Guests")
            .Include("Rentals")
            .Include("Stay");

        /// <summary>
        /// Updates a `BookingModel` record
        /// </summary>
        /// <param name="booking">
        /// A `BookingModel` object whose properties match the values of a row
        /// from the `Bookings` table
        /// </param>
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
