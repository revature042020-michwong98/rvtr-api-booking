namespace RVTR.Booking.DataContext
{
    /// <summary>
    /// A POCO used to specify the possible queries used for filtering/sorting
    /// records pulled from the db
    /// </summary>
    public class SearchFilterQueries
    {
        private string _filter;
        /// <summary>
        /// The expression used to filter data fetched based on a
        /// specified constraint
        /// #### Usage
        /// `/{resource}?filter= id == 1`
        /// </summary>
        /// <value></value>
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
        /// <summary>
        /// Defines what property of the resource should be used to sort
        /// </summary>
        /// <value></value>
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
        /// <summary>
        /// Defines the amount of records to skip
        /// </summary>
        /// <value></value>
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
        /// <summary>
        /// Specifies the amount of records that should be returned
        /// </summary>
        /// <value></value>
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
