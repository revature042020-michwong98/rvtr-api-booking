namespace RVTR.Booking.DataContext
{
    public class BookingSearchQueries : SearchFilterQueries
    {
        // Overrides for properties in SearchFilterQueries and additional properties.
        private string _accountId;
        public string AccountId
        {
            get { return _accountId; }
            set
            {
                _accountId = value;
            }
        }
    }
}