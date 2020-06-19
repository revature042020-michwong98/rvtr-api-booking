using RVTR.Booking.DataContext;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  
  public class StaySearchFilterTest
  {
    public static readonly IEnumerable<Object[]> _dateFilterQueries = new List<Object[]>
    {
      new object[]
      {
        "2020-01-01"
      },
      new object[]
      {
        ""
      },
      new object[]
      {
        "2020-01-05to2020-01-01"
      },
      new object[]
      {
        "intoout"
      },
      new object[]
      {
        "2020-01-05toout"
      }
    };

    public static readonly IEnumerable<Object[]> _searchFilterQueries = new List<Object[]>
    {
      new object[]
      {
        new StaySearchQueries
        {
          Filter = "id==1",
          Limit = "4",
          Offset = "5",
          Sort = "id"
        }
      },
      new object[]
      {
        new StaySearchQueries
        {
          Filter = null,
          Limit = "1",
          Offset = "0",
          Sort = null
        }    
      }
      };

    public static readonly StaySearchQueries _singleSearchFilterQuery = new StaySearchQueries()
    {
 
      Filter = "id==1",
      Limit = "1",
      Offset = "0",
      Sort = "id"
    };

    [Theory]
    [MemberData(nameof(_dateFilterQueries))]
    public void Test_StaySearchFilter_CreateDateFilter(string dateFilter)
    {
      var staySearchFilter = new StaySearchFilter(_singleSearchFilterQuery);
      staySearchFilter.CreateDateFilter(dateFilter);
      Assert.Empty(staySearchFilter.Filters);
    }

    [Fact]
    public void Test_StaySearchFilter_CreateLodgingIdFilter()
    {
      var staySearchFilter = new StaySearchFilter(_singleSearchFilterQuery);
      staySearchFilter.CreateLodgingIdFilter("");
      Assert.Empty(staySearchFilter.Filters);
    }

    [Fact]
    public void Test_StaySearchFilter_Successful_CreateDateFilter()
    {
      var staySearchFilter = new StaySearchFilter(_singleSearchFilterQuery);
      staySearchFilter.CreateDateFilter("2020-01-01to2020-01-03");
      Assert.Equal(2, staySearchFilter.Filters.Count);
      Assert.Equal("1/1/2020 12:00:00 AM", staySearchFilter.CheckIn.ToString());
      Assert.Equal("1/3/2020 12:00:00 AM", staySearchFilter.CheckOut.ToString());
    }

    [Theory]
    [MemberData(nameof(_searchFilterQueries))]
    public void Test_StaySearchFilter_Successful_CreateLodgingIdFilter(StaySearchQueries staySearchQueries)
    {
      var staySearchFilter = new StaySearchFilter(staySearchQueries);
      staySearchFilter.CreateLodgingIdFilter("1");
      Assert.Single(staySearchFilter.Filters);
      Assert.Equal(1, staySearchFilter.LodgingId);
    }

    [Fact]
    public void Test_StaySearchQueries()
    {
      var bookingSearchQuery = new StaySearchQueries()
      {
        Dates = "2020-01-01to2020-01-03",
        Limit = "5",
        LodgingId = "2",
        Filter = "id == 1",
        Offset = "0",
        Sort = "id"
      };
      Assert.Equal("2020-01-01to2020-01-03", bookingSearchQuery.Dates);
      Assert.Equal("2", bookingSearchQuery.LodgingId);
    }

  }
}
