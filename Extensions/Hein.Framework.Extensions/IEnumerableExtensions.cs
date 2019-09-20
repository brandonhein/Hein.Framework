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
    }
}
