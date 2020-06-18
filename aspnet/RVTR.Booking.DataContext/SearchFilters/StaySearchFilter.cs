using RVTR.Booking.ObjectModel.Models;


namespace RVTR.Booking.DataContext
{
  /// <summary>
  /// Filter model for defining properties
  /// used to filter, sort and paginate Stays
  /// </summary>
  public class StaySearchFilter : SearchFilter<StayModel>
  {
    public StaySearchFilter(StaySearchQueries queries) : base(queries)
    {

    }
  }
}
