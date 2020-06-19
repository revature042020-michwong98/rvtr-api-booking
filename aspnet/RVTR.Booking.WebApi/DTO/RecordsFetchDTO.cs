using System.Collections.Generic;

namespace RVTR.Booking.WebApi.Controllers
{
  /// <summary>
  /// Data transfer object for dispatching data
    /// regarding multiple entity records and
    /// the total amount of entities for pagination
    /// purposes.
  /// </summary>
  public class RecordsFetchDTO<TEntity> where TEntity : class
  {
    /// <summary>
    /// List of entity records
    /// </summary>
    public IEnumerable<TEntity> Records { get; set; }

    /// <summary>
    /// Count of all entity records stored in the
    /// databse.
    /// </summary>
    public int Total { get; set; } = 0;

    /// <summary>
    /// Data transfer object for dispatching data
    /// regarding multiple entity records and
    /// the total amount of entities for pagination
    /// purposes.
    /// </summary>
    public RecordsFetchDTO() { }
  }
}
