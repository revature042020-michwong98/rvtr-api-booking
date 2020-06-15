using System;
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

        private Repository<BookingModel> _booking;
        public virtual Repository<BookingModel> Booking
        {
            get
            {
              if (this._booking == null)
                this._booking = new Repository<BookingModel>(this._context);
              return _booking;
            }
        }

        private Repository<StayModel> _stay;
        public virtual Repository<StayModel> Stay {
          get
          {
            if (this._stay == null)
              this._stay = new Repository<StayModel>(this._context);
            return _stay;
          }
        }

        public UnitOfWork(BookingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Represents the _UnitOfWork_ `Commit` method
        /// </summary>
        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
    }
}