using System;

namespace Hein.Framework.Extensions
{
    public static class NumberExtensions
    {
        public static decimal SetPrecision(this decimal val, int precision)
        {
            return decimal.Round(val, precision, MidpointRounding.AwayFromZero);
        }

        public static double SetPrecision(this double val, int precision)
        {
            return (double)SetPrecision((decimal)val, precision);
        }

        public static bool IsBetween(this decimal value, decimal from, decimal to)
        {
            return value >= from && value <= to;
        }

        public static bool IsBetween(this decimal? value, decimal from, decimal to)
        {
            return value.HasValue && value.Value >= from && value.Value <= to;
        }

        public static bool IsBetween(this double value, double from, double to)
        {
            return value >= from && value <= to;
        }

        public static bool IsBetween(this double? value, double from, double to)
        {
            return value.HasValue && value.Value >= from && value.Value <= to;
        }

        public static bool IsBetween(this int value, int from, int to)
        {
            return value >= from && value <= to;
        }

        public static bool IsBetween(this int? value, int from, int to)
        {
            return value.HasValue && value.Value >= from && value.Value <= to;
        }

        public static bool IsBetween(this short value, short from, short to)
        {
            return value >= from && value <= to;
        }

        public static bool IsBetween(this short? value, short from, short to)
        {
            return value.HasValue && value.Value >= from && value.Value <= to;
        }
    }
}
