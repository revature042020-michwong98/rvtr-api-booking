namespace RVTR.Booking.DataContext
{
    public class SearchFilterQueries
    {
        private string _filter;
        public virtual string Filter
        {
            get
            {
                return _filter;
            } 
            set
            {
                _filter = value;
            }
        }

        private string _sort;
        public virtual string Sort
        {
            get
            {
                return _sort;
            } 
            set
            {
                _sort = value;
            }
        }

        private string _offset;
        public virtual string Offset
        {
            get
            {
                return _offset;
            } 
            set
            {
                _offset = value;
            }
        }

        private string _limit;
        public virtual string Limit
        {
            get
            {
                return _limit;
            } 
            set
            {
                _limit = value;
            }
        }
    }
}