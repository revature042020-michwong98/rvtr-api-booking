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
        

        public StaySearchFilter(StaySearchQueries staySearchQueries) : base(staySearchQueries)
        {
            this.Includes="Booking";

            CreateDuringFilter(staySearchQueries.Dates);
        }

        public virtual void CreateDuringFilter(string duringString)
        {
            if (String.IsNullOrEmpty(duringString))
                return;

            var dateStrings = duringString.Split(new string[] {"to"}, System.StringSplitOptions.RemoveEmptyEntries);
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
    }
}
