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
            booking.BookingRentals = booking.BookingRentals.Distinct().ToList();
            foreach (var bookingRental in booking.BookingRentals)
            {
                var rental = await _context.Set<RentalModel>().AsNoTracking().FirstOrDefaultAsync(r => r.Id == bookingRental.RentalId);
                if (rental == null)
                {
                    var newRental = (await _context.AddAsync(new RentalModel { Id = bookingRental.RentalId })).Entity;
                    bookingRental.Rental = newRental;
                }
            }
            await _db.AddAsync(booking).ConfigureAwait(true);
        }

        public override async Task<IEnumerable<BookingModel>> SelectAsync() => await IncludeQuery().ToListAsync();

        public override async Task<BookingModel> SelectAsync(int id) => await IncludeQuery().FirstAsync(booking => booking.Id == id);

        private IQueryable<BookingModel> IncludeQuery()
            => _db.Include("Guests")
            .Include("BookingRentals")
            .Include("Stay");

        public override void Update(BookingModel booking)
        {
            var bookingEnity = IncludeQuery().Include("BookingRentals").FirstOrDefault(be => be.Id == booking.Id);
            var bookingRentalEntities = _context.BookingRentals.Include(br => br.Rental).Where(br => br.BookingId == booking.Id).ToList();
            var bookingRentals = booking.BookingRentals;

            foreach (var bookingRentalEntity in bookingRentals)
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
            _db.Update(bookingEnity);
        }
    }
}
