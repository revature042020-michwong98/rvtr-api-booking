using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
    public class StayRepository : Repository<StayModel>
    {
        public StayRepository(BookingContext context) : base(context) { }
    }
}
