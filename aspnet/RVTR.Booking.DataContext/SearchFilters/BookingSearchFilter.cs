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

        public BookingSearchFilter(BookingSearchQueries bookingSearchQueries)
            : base(bookingSearchQueries)
        {
            Includes = "Guests,Rentals";

            CreateAccountIdFilter(bookingSearchQueries?.AccountId);
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
    }
}
