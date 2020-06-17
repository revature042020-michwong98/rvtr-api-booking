using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
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

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int offset = 0, int limit = 50)
        {
            var searchFilter = new SearchFilter<TEntity>()
            {
                Filters = new List<Expression<Func<TEntity, bool>>>() { filter },
                OrderBy = orderBy,
                Includes = includeProperties,
                Offset = offset,
                Limit = limit
            };
            return await SelectAsync(searchFilter);
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(SearchFilter<TEntity> searchFilter)
        {
            IQueryable<TEntity> query = _db;

            if (searchFilter.Filters != null)
            {
                foreach (var filter in searchFilter.Filters)
                {
                    if (filter == null)
                        continue;
                    query = query.Where(filter);
                }
            }

            if (!String.IsNullOrEmpty(searchFilter.StringFilter))
                query = query.Where(searchFilter.StringFilter);

            if (!string.IsNullOrEmpty(searchFilter.Includes))
                foreach (var includeProperty in searchFilter.Includes.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            if (searchFilter.OrderBy != null)
                return await searchFilter.OrderBy(query).Skip(searchFilter.Offset).Take(searchFilter.Limit).ToListAsync();
            return await query.OrderBy(e => e).Skip(searchFilter.Offset).Take(searchFilter.Limit).ToListAsync();
        }

        public virtual void Update(TEntity entry) => _db.Update(entry);
    }
}
