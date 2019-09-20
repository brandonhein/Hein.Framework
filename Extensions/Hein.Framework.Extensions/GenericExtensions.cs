using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T source, IEnumerable<T> values)
        {
            if (values == null)
            {
                return false;
            }

            return IsOneOf(source, values.ToArray());
        }

        public static bool IsOneOf<T>(this T source, params T[] comparisonValue)
        {
            bool exists = false;

            if (source != null && comparisonValue != null && comparisonValue.Any())
            {
                exists = comparisonValue.Contains(source);
            }

            return exists;
        }

        public static T ToType<T>(this object value)
        {
            var conversionType = typeof(T);
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }
                conversionType = Nullable.GetUnderlyingType(conversionType);
            }
            return (T)Convert.ChangeType(value, conversionType);
        }
    }
}
