using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T obj in collection)
            {
                action(obj);
            }
        }

        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int topCount)
        {
            return collection.Take(topCount);
        }

        public static IEnumerable<T> Last<T>(this IEnumerable<T> collection, int lastCount)
        {
            return collection.Skip(Math.Max(0, collection.Count() - lastCount));
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Any();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            var comparer = new GeneralPropertyComparer<T, TKey>(property);
            return items.Distinct(comparer);
        }

        public static IEnumerable<IEnumerable<T>> ToBatch<T>(this IEnumerable<T> items, int batchSize)
        {
            var result = new List<IEnumerable<T>>();
            var batch = new List<T>();
            foreach (var item in items)
            {
                batch.Add(item);
                if (batch.Count == batchSize)
                {
                    result.Add(batch);
                    batch = new List<T>();
                }
            }

            //hit up the last of the batch if extras
            if (batch.Any())
            {
                result.Add(batch);
            }

            return result;
        }
    }

    public class GeneralPropertyComparer<T, TKey> : IEqualityComparer<T>
    {
        private Func<T, TKey> expr { get; set; }
        public GeneralPropertyComparer(Func<T, TKey> expr)
        {
            this.expr = expr;
        }
        public bool Equals(T x, T y)
        {
            var leftProp = expr.Invoke(x);
            var rightProp = expr.Invoke(y);
            if (leftProp == null && rightProp == null)
                return true;
            else if (leftProp == null ^ rightProp == null)
                return false;
            else
                return leftProp.Equals(rightProp);
        }
        public int GetHashCode(T obj)
        {
            var prop = expr.Invoke(obj);
            return (prop == null) ? 0 : prop.GetHashCode();
        }
    }
}
