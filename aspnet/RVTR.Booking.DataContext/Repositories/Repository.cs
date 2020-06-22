using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Booking.DataContext.Repositories
{
    /// <summary>
    /// Generic repository providing standard CRUD operations for
    /// retreiving data from the Db
    /// </summary>
    /// <typeparam name="TEntity">The object model used with the ORM</typeparam>
    public class Repository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The generic set that refers to the entity `ObjectModel`
        /// used to query the Db in regards to this Model and it's
        /// related entities.
        /// </summary>
        public readonly DbSet<TEntity> _db;

        /// <summary>
        /// The database context used to perform operations
        /// to query data from the Db
        /// </summary>
        protected readonly BookingContext _context;

        /// <summary>
        /// Generic repository providing standard CRUD operations for
        /// retreiving data from the Db
        /// </summary>
        /// <param name="context">
        /// Db context used to perform operations against the database.
        /// </param>
        public Repository(BookingContext context)
        {
            _db = context.Set<TEntity>();
            _context = context;
        }

        /// <summary>
        /// Removes record from Db
        /// </summary>
        /// <param name="id">Record's unique id</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(int id) => _db.Remove(await SelectAsync(id));

        /// <summary>
        /// Inserts a new record to Db
        /// </summary>
        /// <param name="entry">
        /// Entry with all valid properties that meets the contstraints of the entity
        /// </param>
        /// <returns></returns>
        public virtual async Task InsertAsync(TEntity entry) => await _db.AddAsync(entry).ConfigureAwait(true);

        /// <summary>
        /// Fetches all records of this entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await _db.ToListAsync();

        public virtual async Task<TEntity> SelectAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

        /// <summary>
        /// Returns all records that match the search filters.
        /// </summary>
        /// <param name="filter">Predicate for linq where method</param>
        /// <param name="orderBy">Order by method for linq order by method</param>
        /// <param name="includeProperties">Properties to be included</param>
        /// <param name="offset">Number for skip</param>
        /// <param name="limit">Number for take</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int offset = 0, int limit = 50)
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

        /// <summary>
        /// Retreives all records of this entity that meets the constraints of the `SearchFilter`
        /// </summary>
        /// <param name="searchFilter">
        /// A POCO whose properties are used to specify the constraints when filtering data
        /// </param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> SelectAsync(SearchFilter<TEntity> searchFilter)
        {
            IQueryable<TEntity> query = _db;

            // Include.
            if (!string.IsNullOrEmpty(searchFilter.Includes))
                foreach (var includeProperty in searchFilter.Includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            // Filter.
            if (searchFilter.Filters != null)
            {
                foreach (var filter in searchFilter.Filters)
                {
                    if (filter == null)
                        continue;
                    query = query.Where(filter);
                }
            }

            // Filter
            try
            {
                if (!String.IsNullOrEmpty(searchFilter.StringFilter))
                    query = query.Where(searchFilter.StringFilter);
            }
            catch
            {
                // Catches parse exceptions.
            }

            // Order By, Skip, Take.
            try
            {
                if (searchFilter.OrderBy != null)
                    return await searchFilter.OrderBy(query).Skip(searchFilter.Offset).Take(searchFilter.Limit).ToListAsync();
            }
            catch
            {
                // Catches parse exceptions.
            }

            return await query.OrderBy(e => e).Skip(searchFilter.Offset).Take(searchFilter.Limit).ToListAsync();
        }

        /// <summary>
        /// Updates a record of the entity
        /// </summary>
        /// <param name="entry">
        /// The ObjectModel of the entity used to update the values of a row from the Db
        /// </param>
        public virtual void Update(TEntity entry) => _db.Update(entry);
    }
}
