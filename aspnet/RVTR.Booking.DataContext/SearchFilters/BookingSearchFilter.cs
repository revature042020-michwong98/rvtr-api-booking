using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{

    public class BookingSearchFilter : SearchFilter<BookingModel>
    {
        private int _accountId;
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
        public int LodgingId
        {
            get { return _lodgingId; }
            set
            {
                if (value > 0)
                    _lodgingId = value;
            }
        }

        public BookingSearchFilter(BookingSearchQueries bookingSearchQueries)
            : base(bookingSearchQueries)
        {
            Includes = "Guests,Rentals";

            CreateAccountIdFilter(bookingSearchQueries?.AccountId);
            CreateLodgingIdFilter(bookingSearchQueries?.LodgingId);
        }

        public BookingSearchFilter() : this(null) { }

        public void CreateAccountIdFilter(string accountIdString)
        {
            int accountId;
            if (!Int32.TryParse(accountIdString, out accountId))
                return;

            this.AccountId = accountId;

            this.Filters.Add(bookingModel => bookingModel.AccountId == this.AccountId);
        }

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
