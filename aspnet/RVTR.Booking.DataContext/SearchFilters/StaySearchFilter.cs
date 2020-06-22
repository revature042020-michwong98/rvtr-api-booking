using System;
using RVTR.Booking.ObjectModel.Models;


namespace RVTR.Booking.DataContext
{
    /// <summary>
    /// Filter model for defining properties
    /// used to filter, sort and paginate Stays
    /// </summary>
    public class StaySearchFilter : SearchFilter<StayModel>
    {
        private DateTime _checkIn;
        public virtual DateTime CheckIn
        {
            get { return _checkIn; }
            set { _checkIn = value; }
        }


        private DateTime _checkOut;
        public virtual DateTime CheckOut
        {
            get { return _checkOut; }
            set { _checkOut = value; }
        }

        private int _lodgingId;
        public int LodgingId
        {
            get { return _lodgingId; }
            set
            {
                if (value > 0)
                    _lodgingId = value;
            }
        }

        public StaySearchFilter(StaySearchQueries staySearchQueries) : base(staySearchQueries)
        {
            this.Includes = "Booking,Booking.BookingRentals";

            CreateDateFilter(staySearchQueries.Dates);
            CreateLodgingIdFilter(staySearchQueries.LodgingId);
        }

        /// <summary>
        /// Adds expressions for querying Stay records to be within a certain date range
        ///
        /// #### Usage
        /// ```
        /// /Stay?dates=2020-01-01 to 2020-02-01
        /// ```
        /// </summary>
        /// <param name="dateString">
        /// String used to determine the date range used
        /// </param>
        public virtual void CreateDateFilter(string dateString)
        {
            if (String.IsNullOrEmpty(dateString))
                return;

            var dateStrings = dateString.Split(new string[] { "to" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (dateStrings.Length < 2)
                return;

            DateTime checkIn, checkOut;
            if (!DateTime.TryParse(dateStrings[0].Trim(), out checkIn))
                return;
            if (!DateTime.TryParse(dateStrings[1].Trim(), out checkOut))
                return;

            if (checkIn > checkOut) return;

            this.CheckIn = checkIn;
            this.CheckOut = checkOut;

            this.Filters.Add(stayModel => stayModel.CheckOut >= this.CheckIn);
            this.Filters.Add(stayModel => stayModel.CheckIn <= this.CheckOut);
        }

        /// <summary>
        /// Adds a filter expression for querying Stay records that inlcude a sepcified
        ///
        /// #### Usage
        /// ```
        /// /Stay?LodgingId=1
        /// ```
        /// </summary>
        /// <param name="lodgingIdString">The provided id of the Lodging record</param>
        public virtual void CreateLodgingIdFilter(string lodgingIdString)
        {
            int lodgingId;
            if (!Int32.TryParse(lodgingIdString, out lodgingId))
                return;

            this.LodgingId = lodgingId;

            this.Filters.Add(stayModel => stayModel.Booking.LodgingId == this.LodgingId);
        }
    }
}
