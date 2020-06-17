using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{

    public class BookingSearchFilter : SearchFilter<BookingModel>
    {
        public BookingSearchFilter(IEnumerable<KeyValuePair<String, StringValues>> queryParameters)
            : base(queryParameters)
        {
            Includes = "Stay,Guests,Rentals";
        }

        public BookingSearchFilter() : this(null) { }
    }
}
