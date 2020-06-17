using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{

    public class BookingSearchFilter : SearchFilter<BookingModel>
    {
        public BookingSearchFilter(SearchFilterQueries searchFilterQueries)
            : base(searchFilterQueries)
        {
            Includes = "Guests,Rentals";
        }

        public BookingSearchFilter() : this(null) { }
    }
}
