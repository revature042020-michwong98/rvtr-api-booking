using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
    public class BookingRepository : Repository<BookingModel>
    {
        public BookingRepository(BookingContext context) : base(context) { }

        public override async Task<IEnumerable<BookingModel>> SelectAsync() => await IncludeQuery().ToListAsync();

        public override async Task<BookingModel> SelectAsync(int id) => await IncludeQuery().FirstOrDefaultAsync(booking => booking.Id == id);

        public override async Task<IEnumerable<BookingModel>> SelectAsync(Expression<Func<BookingModel, bool>> filter = null, Func<IQueryable<BookingModel>, IOrderedQueryable<BookingModel>> orderBy = null, int limit = 50, int offset = 0)
            => await SelectAsync(filter, orderBy, "Stay,Rentals,Guests", limit, offset);
        private IQueryable<BookingModel> IncludeQuery()
            => _db.Include("Stay")
            .Include("Guests")
            .Include("Rentals");
    }
}