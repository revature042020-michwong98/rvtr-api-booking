using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{
    /// <summary>
    /// A utility used to filter and sort records fetched from the database
    /// </summary>
    public class BookingSearchFilter : SearchFilter<BookingModel>
    {
        private int _accountId;
        /// <summary>
        /// Id of the account record related to the booking record
        /// </summary>
        /// <value></value>
        public int AccountId
        {
            get { return _accountId; }
            set
            {
                if (value > 0)
                    _accountId = value;
            }
        }

        private int _lodgingId;
        /// <summary>
        /// Id of the lodging record related to the booking record
        /// </summary>
        /// <value></value>
        public int LodgingId
        {
            get { return _lodgingId; }
            set
            {
                if (value > 0)
                    _lodgingId = value;
            }
        }

        /// <summary>
        /// Utility used to filter and sort records fetched from the database.
        /// </summary>
        /// <param name="bookingSearchQueries">
        /// A POCO that specifies which fitlers/sorts to use to fetch records
        /// </param>
        public BookingSearchFilter(BookingSearchQueries bookingSearchQueries)
            : base(bookingSearchQueries)
        {
            Includes = "Guests,BookingRentals,BookingRentals,Stay";

            CreateAccountIdFilter(bookingSearchQueries?.AccountId);
            CreateLodgingIdFilter(bookingSearchQueries?.LodgingId);
        }

        public BookingSearchFilter() : this(null) { }

        /// <summary>
        /// TODO: add details
        /// </summary>
        /// <param name="accountIdString">Account's id pulled from query</param>
        public void CreateAccountIdFilter(string accountIdString)
        {
            int accountId;
            if (!Int32.TryParse(accountIdString, out accountId))
                return;

            this.AccountId = accountId;

            this.Filters.Add(bookingModel => bookingModel.AccountId == this.AccountId);
        }

        /// <summary>
        /// TODO: add details
        /// </summary>
        /// <param name="lodgingIdString">Lodgin's id pulled from query</param>
        public void CreateLodgingIdFilter(string lodgingIdString)
        {
            int lodgingId;
            if (!Int32.TryParse(lodgingIdString, out lodgingId))
                return;

            this.LodgingId = lodgingId;

            this.Filters.Add(bookingModel => bookingModel.LodgingId == this.LodgingId);
        }
    }
}
