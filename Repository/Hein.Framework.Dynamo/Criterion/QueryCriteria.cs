﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Amazon.DynamoDBv2;

namespace Hein.Framework.Dynamo.Criterion
{
    public class QueryCriteria<T> : DynamoQueryCollection, IQueryCriteria<T>
    {
        public IQueryCriteria<T> Select(params string[] propertyNames)
        {
            base.SetupRequest<T>();
            _queryRequest.AttributesToGet = propertyNames.ToList();

            return this;
        }

        public IQueryCriteria<T> Select<M>()
        {
            base.SetupRequest<T>();
            var columns = string.Empty;
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
                    columns = string.Concat(columns, prop.Name, ",");
                }
            }

            columns = columns.TrimEnd(',');
            var columnList = columns.Split(',').ToList();

            _queryRequest.AttributesToGet = columnList;

            return this;
        }

        public IQueryCriteria<T> Distinct()
        {
            return this;
        }

        public IQueryCriteria<T> Top(int max)
        {
            base.SetupRequest<T>();
            _queryRequest.Limit = max;
            return this;
        }

        public IQueryCriteria<T> Where(Expression<Func<T, bool>> filter)
        {
            base.SetupRequest<T>();

            var expressionString = new StringBuilder();
            _queryRequest.ExpressionAttributeValues = ExpressionHelper.ConvertExpressionValues(filter, ref expressionString);
            _queryRequest.FilterExpression = expressionString.ToString();


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
            return this;
        }
    }
}
