namespace RVTR.Booking.DataContext
{
    public class StaySearchQueries : SearchFilterQueries
    {
        private string _dates;
        public virtual string Dates
        {
            get { return _dates; }
            set { _dates = value; }
        }   
    }
}



// ?during=yyyy-MM-dd to yyyy-MM-dd