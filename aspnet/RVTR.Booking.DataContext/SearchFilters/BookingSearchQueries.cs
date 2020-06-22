namespace RVTR.Booking.DataContext
{
    public class BookingSearchQueries : SearchFilterQueries
    {
        // Overrides for properties in SearchFilterQueries and additional properties.
        public string AccountId { get; set; }

        public string LodgingId { get; set; }
    }
}