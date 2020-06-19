using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using RVTR.Booking.DataContext;
using RVTR.Booking.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class SearchFilterTest
  {
    [Fact]
    public void Test_SearchFilter_GenerateFilter()
    {
      var bookingSearchQuery = new BookingSearchQueries()
      {
        AccountId = "1",
        Limit = "5",
        LodgingId = "2",
        Filter = "order-by",
        Offset = "0",
        Sort = "none"
      };
      var searchFilter = new SearchFilter<BookingModel>();
      
    }

  }
}
