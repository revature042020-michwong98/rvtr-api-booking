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

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        public virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }

        private Expression<Func<TEntity, bool>> _filter;
        public virtual Expression<Func<TEntity, bool>> Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public SearchFilter(IEnumerable<KeyValuePair<String, StringValues>> queryParameters)
        {
            int limit;
            if (Int32.TryParse(queryParameters.FirstOrDefault(x => x.Key == "offset").Value, out limit))
                Limit = limit;
            int offset;
            if (Int32.TryParse(queryParameters.FirstOrDefault(x => x.Key == "limit").Value, out offset))
                Offset = offset;
            
            GenerateOrderby(queryParameters.FirstOrDefault(x => x.Key == "sort").Value);
        }

        private void GenerateOrderby(string sortString = null)
        {
            if (String.IsNullOrEmpty(sortString))
                return;
            string sort = ParseSort(sortString);
            if (!String.IsNullOrEmpty(sort))
            {
                OrderBy = (queryable) => queryable.OrderBy(sort);
            }
        }

        protected virtual string ParseSort(string sortString)
        {
            return sortString;
        }
    }

}