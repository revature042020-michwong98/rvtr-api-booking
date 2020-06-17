using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Booking.DataContext
{

    public class SearchFilter<TEntity> where TEntity : class
    {
        private Expression<Func<TEntity, bool>> _expressionFilter;
        public virtual Expression<Func<TEntity, bool>> ExpressionFilter
        {
            get { return _expressionFilter; }
            set { _expressionFilter = value; }
        }

        private string _stringFilter;
        public virtual string StringFilter
        {
            get { return _stringFilter; }
            set { _stringFilter = value; }
        }

        private string _includes;
        public virtual string Includes
        {
            get { return _includes; }
            set { _includes = value; }
        }

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        public virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }

        private int _offset = 0;
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

        public SearchFilter(SearchFilterQueries searchFilterQueries)
        {
            if (searchFilterQueries != null)
            {
                GenerateOrderby(searchFilterQueries.Sort);

                int offset;
                if (Int32.TryParse(searchFilterQueries.Offset, out offset))
                    Offset = offset;
                    
                int limit;
                if (Int32.TryParse(searchFilterQueries.Limit, out limit))
                    Limit = limit;
            }
        }

        private void GenerateFilter(string filterString = null)
        {
            if (String.IsNullOrEmpty(filterString))
                return;
            string filter = ParseFilter(filterString);
            if (!String.IsNullOrEmpty(filter))
                StringFilter = filter;
        }

        protected virtual string ParseFilter(string filterString) => filterString;

        private void GenerateOrderby(string sortString = null)
        {
            if (String.IsNullOrEmpty(sortString))
                return;
            string sort = ParseSort(sortString);
            if (!String.IsNullOrEmpty(sort))
                OrderBy = x => x.OrderBy(sort);

        }

        protected virtual string ParseSort(string sortString) => sortString;
    }

}
