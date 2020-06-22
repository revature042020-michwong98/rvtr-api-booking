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
        public override async Task InsertAsync(BookingModel entry)
        {
            entry.BookingRentals = entry.BookingRentals.Distinct().ToList();
            foreach (var bookingRental in entry.BookingRentals)
            {
                var rental = await _context.Set<RentalModel>().AsNoTracking().FirstOrDefaultAsync(r => r.Id == bookingRental.RentalId);
                if (rental == null)
                {
                    var newRental = (await _context.AddAsync(new RentalModel { Id = bookingRental.RentalId })).Entity;
                    bookingRental.Rental = newRental;
                }
            }
            await _db.AddAsync(entry).ConfigureAwait(true);
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
            .Include("BookingRentals")
            .Include("Stay");

        /// <summary>
        /// Updates a `BookingModel` record
        /// </summary>
        /// <param name="booking">
        /// A `BookingModel` object whose properties match the values of a row
        /// from the `Bookings` table
        /// </param>
        public override void Update(BookingModel entry)
        {
            var bookingEnity = IncludeQuery().Include("BookingRentals").FirstOrDefault(be => be.Id == entry.Id);
            var bookingRentalEntities = _context.BookingRentals.Include(br => br.Rental).Where(br => br.BookingId == entry.Id).ToList();
            var bookingRentals = entry.BookingRentals;

            foreach (var bookingRentalEntity in bookingRentalEntities)
            {
                var bookingRental = bookingRentals.FirstOrDefault(br => br.RentalId == bookingRentalEntity.RentalId);
                if (bookingRental == null)
                {
                    _context.BookingRentals.Remove(bookingRentalEntity);
                    _context.Entry(bookingRentalEntity).State = EntityState.Deleted;
                }
            }

            var newBookingRentals = new List<BookingRentalModel>();

            foreach (var bookingRental in bookingRentals)
            {
                var bookingRentalEntity = bookingRentalEntities.FirstOrDefault(bre => bre.RentalId == bookingRental.RentalId);
                if (bookingRentalEntity == null)
                {
                    var rental = _context.Set<RentalModel>().AsNoTracking().FirstOrDefault(r => r.Id == bookingRental.RentalId);
                    if (rental == null)
                    {
                        var newRental = _context.Add(new RentalModel { Id = bookingRental.RentalId }).Entity;
                        bookingRental.Rental = newRental;
                    }
                    newBookingRentals.Add(bookingRental);
                }
                else
                {
                    newBookingRentals.Add(bookingRentalEntity);
                }
            }
            bookingEnity.BookingRentals = newBookingRentals;

            var guests = _context.Set<GuestModel>().Where(g => g.BookingId == entry.Id).ToList();
            var newGuests = new List<GuestModel>();
            _context.RemoveRange(guests);
            
            foreach (var guest in entry.Guests)
            {
                guest.BookingId = entry.Id;
                _context.Add(guest);
                newGuests.Add(guest);
            }

            entry.Guests = newGuests;

            _db.Update(bookingEnity);
        }
    }
}
