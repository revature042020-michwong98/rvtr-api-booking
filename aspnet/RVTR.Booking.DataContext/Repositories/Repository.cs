using System.Collections.Generic;

namespace RVTR.Booking.DataContext.Repositories
{
  public class Repository<TEntity> where TEntity : class
  {
    public bool Delete(int id) => throw new System.NotImplementedException();

    public bool Insert(TEntity entity) => throw new System.NotImplementedException();

    public IEnumerable<TEntity> Select() => throw new System.NotImplementedException();

    public TEntity Select(int id) => throw new System.NotImplementedException();

    public bool Update(TEntity entity) => throw new System.NotImplementedException();
  }
}
