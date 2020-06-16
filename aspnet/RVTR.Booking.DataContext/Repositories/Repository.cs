using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Booking.DataContext.Repositories
{
    /// <summary>
    /// Represents the _Repository_ generic
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> where TEntity : class
    {
        public readonly DbSet<TEntity> _db;

        public Repository(BookingContext context)
        {
            _db = context.Set<TEntity>();
        }

        public virtual async Task DeleteAsync(int id) => _db.Remove(await SelectAsync(id));

        public virtual async Task InsertAsync(TEntity entry) => await _db.AddAsync(entry).ConfigureAwait(true);

        public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await _db.ToListAsync();

        public virtual async Task<TEntity> SelectAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int limit = 50, int offset = 0)
        {
            IQueryable<TEntity> query = _db;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            if (orderBy != null)
                return await orderBy(query).Skip(offset).Take(limit).ToListAsync();
            return await query.Skip(offset).Take(limit).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int limit = 50, int offset = 0)
            => await SelectAsync(filter, orderBy, null, limit, offset);


        public virtual void Update(TEntity entry) => _db.Update(entry);
    }
}