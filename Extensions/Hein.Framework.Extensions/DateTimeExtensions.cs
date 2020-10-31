using System;

namespace Hein.Framework.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime value, DateTime from, DateTime to)
        {
            return value >= from && value <= to;
        }

        public static bool IsBetween(this DateTime? value, DateTime from, DateTime to)
        {
            return value.HasValue && value >= from && value <= to;
        }
    }
}
