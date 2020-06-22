namespace RVTR.Booking.DataContext
{
    /// <summary>
    /// A POCO that states which queries can be used when searching
    /// for `StayModel` records from the API
    /// </summary>
    public class StaySearchQueries : SearchFilterQueries
    {
        /// <summary>
        /// Query to search for Stays within a certain date range
        ///
        /// #### Usage
        /// ```
        /// ?dates=yyyy-MM-dd to yyyy-MM-dd
        /// ```
        /// </summary>
        private string _dates;
        public virtual string Dates
        {
            get { return _dates; }
            set { _dates = value; }
        }

        /// <summary>
        /// Query used to find Stays that include this Lodging Id
        ///
        /// #### Usage
        /// ```
        /// ?lodgingId=1
        /// ```
        /// </summary>
        private string _lodgingId;
        public string LodgingId
        {
            get { return _lodgingId; }
            set { _lodgingId = value; }
        }

    }
}



// ?during=yyyy-MM-dd to yyyy-MM-dd
