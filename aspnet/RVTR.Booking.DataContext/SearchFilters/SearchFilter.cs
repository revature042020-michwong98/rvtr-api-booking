using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;

namespace RVTR.Booking.DataContext
{
    /// <summary>
    /// The search filter class is a utility used to filter and sort records fetched from the database.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SearchFilter<TEntity> where TEntity : class
    {
        private List<Expression<Func<TEntity, bool>>> _filters = new List<Expression<Func<TEntity, bool>>>();
        /// <summary>
        /// The filter is an expression which is used to filter data fetched that
        /// meets a specific contstraint.
        /// </summary>
        /// <value></value>
        public virtual List<Expression<Func<TEntity, bool>>> Filters
        {
            get { return _filters; }
            set
            {
                if (value != null)
                    _filters = value;
            }
        }

        private string _stringFilter;
        /// <summary>
        /// Linq filter as a string.
        /// </summary>
        /// <value></value>
        public virtual string StringFilter
        {
            get { return _stringFilter; }
            set
            {
                if (value != null)
                    _stringFilter = value;
            }
        }

        private string _includes;
        /// <summary>
        /// String containing properties to include.
        /// </summary>
        /// <value></value>
        public virtual string Includes
        {
            get { return _includes; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _includes = value;
            }
        }

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        /// <summary>
        /// An expression used to order the records fetched.
        /// </summary>
        /// <value></value>
        public virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy
        {
            get { return _orderBy; }
            set
            {
                if (value != null)
                    _orderBy = value;
            }
        }

        private int _offset = 0;
        /// <summary>
        /// Number used to specify the amount of records that should be skipped
        /// when fetching the data.
        /// </summary>
        /// <value></value>
        public virtual int Offset
        {
            get { return _offset; }
            set
            {
                if (value > 0)
                    _offset = value;
            }
        }

        private int _limit = 50;
        /// <summary>
        /// The amount of records that should be returned.
        /// </summary>
        /// <value></value>
        public virtual int Limit
        {
            get { return _limit; }
            set
            {
                if (value > 0 && value < 300)
                    _limit = value;
            }
        }

        public SearchFilter() { }
        /// <summary>
        /// The search filter class is a utility used to filter and sort records fetched from the database.
        /// </summary>
        /// <param name="searchFilterQueries">
        /// A POCO that specifies which fitlers/sorts to use to fetch records
        /// </param>
        public SearchFilter(SearchFilterQueries searchFilterQueries)
        {
            if (searchFilterQueries != null)
            {
                GenerateFilter(searchFilterQueries.Filter);

                GenerateOrderby(searchFilterQueries.Sort);

                int offset;
                if (Int32.TryParse(searchFilterQueries.Offset, out offset))
                    Offset = offset;

                int limit;
                if (Int32.TryParse(searchFilterQueries.Limit, out limit))
                    Limit = limit;
            }
        }

        /// <summary>
        /// Parses filter string and generates filter expression.
        /// </summary>
        /// <param name="filterString"></param>
        private void GenerateFilter(string filterString = null)
        {
            if (String.IsNullOrEmpty(filterString))
                return;
            string filter = ParseFilter(filterString);
            if (!String.IsNullOrEmpty(filter))
                StringFilter = filter;
        }

        /// <summary>
        /// Parses filter string. For overriding.
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        protected virtual string ParseFilter(string filterString) => filterString;

        private void GenerateOrderby(string sortString = null)
        {
            if (String.IsNullOrEmpty(sortString))
                return;
            string sort = ParseSort(sortString);
            if (!String.IsNullOrEmpty(sort))
                OrderBy = x => x.OrderBy(sort);

        }

        /// <summary>
        /// Parses sort string. For overriding.
        /// </summary>
        /// <param name="sortString"></param>
        /// <returns></returns>
        protected virtual string ParseSort(string sortString) => sortString;
    }

}
