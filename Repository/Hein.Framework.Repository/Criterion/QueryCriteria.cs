using Hein.Framework.Repository.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hein.Framework.Repository.Criterion
{
    public class QueryCriteria<T> : SqlCommandBase, IQueryCriteria<T>
    {
        public override string SQL
        {
            get
            {
                var sqlString = string.Concat("USE ", base.Database);

                sqlString = string.Concat(sqlString, "SELECT",
                    _isDistinct
                        ? " DISTINCT"
                        : string.Empty,
                    _topMost.HasValue
                        ? string.Concat(" TOP ", _topMost.Value)
                        : string.Empty,
                    " {0} FROM ", typeof(T).Name, " (NOLOCK)");

                var columnString = string.Empty;
                if (string.IsNullOrEmpty(_columnString))
                {
                    var columns = new List<string>();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        bool isExcludeProp = false;
                        foreach (var attr in prop.GetCustomAttributes(true))
                        {
                            ExcludeAttribute ex = attr as ExcludeAttribute;
                            if (ex != null)
                            {
                                isExcludeProp = true;
                                break;
                            }
                        }

                        if (!isExcludeProp)
                        {
                            columns.Add(prop.Name);
                        }
                    }

                    columnString = string.Join(",", columns.ToArray());
                }
                else
                {
                    columnString = _columnString;
                }

                sqlString = string.Format(sqlString, columnString);

                if (!string.IsNullOrEmpty(this._whereString))
                {
                    sqlString = string.Concat(sqlString, " WHERE ", _whereString);
                }

                if (!string.IsNullOrEmpty(this._orderByString))
                {
                    sqlString = string.Concat(sqlString, " ORDER BY ", _orderByString, " ", _orderByType.ToString());
                }

                return sqlString;
            }
        }

        private int? _topMost { get; set; }
        private bool _isDistinct { get; set; }
        private string _columnString { get; set; }
        private string _whereString { get; set; }
        private string _orderByString { get; set; }
        private OrderByType _orderByType { get; set; }

        public IQueryCriteria<T> Select<M>()
        {
            _columnString = string.Empty;
            var entityColumns = typeof(T).GetProperties().Select(x => x.Name);
            foreach (var prop in typeof(M).GetProperties())
            {
                bool isExcludeProp = false;
                foreach (var attr in prop.GetCustomAttributes(true))
                {
                    ExcludeAttribute ex = attr as ExcludeAttribute;
                    if (ex != null)
                    {
                        isExcludeProp = true;
                        break;
                    }
                }

                if (!isExcludeProp || entityColumns.Any(x => x == prop.Name))
                {
                    _columnString = string.Concat(_columnString, prop.Name, ",");
                }
            }

            _columnString = _columnString.TrimEnd(',');

            return this;
        }

        public IQueryCriteria<T> Distinct()
        {
            _isDistinct = true;

            return this;
        }

        public IQueryCriteria<T> Top(int max)
        {
            _topMost = max;

            return this;
        }

        public IQueryCriteria<T> Where(Expression<Func<T, bool>> filter)
        {
            var query = new CriterionBuilder<T>();
            var where = query.Where(filter);

            _whereString = query.Translate(where);

            return this;
        }

        public IQueryCriteria<T> OrderBy(Expression<Func<T, object>> orderBy)
        {
            return OrderBy(orderBy, OrderByType.Asc);
        }

        public IQueryCriteria<T> OrderByDesending(Expression<Func<T, object>> orderBy)
        {
            return OrderBy(orderBy, OrderByType.Desc);
        }

        private IQueryCriteria<T> OrderBy(Expression<Func<T, object>> orderBy, OrderByType type)
        {
            var query = new CriterionBuilder<T>();
            var order = query.Order(orderBy);

            _orderByString = query.Translate(order);

            _orderByType = type;

            return this;
        }
    }
}
