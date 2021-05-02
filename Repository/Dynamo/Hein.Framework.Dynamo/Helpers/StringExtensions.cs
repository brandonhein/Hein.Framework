using System;
namespace Hein.Framework.Dynamo.Helpers
{
    internal static class StringExtensions
    {
        public static bool IsOneOf(this string val, params string[] comparisonValue)
        {
            if (!string.IsNullOrEmpty(val))
            {
                foreach (var s in comparisonValue)
                {
                    if (!string.IsNullOrEmpty(s) &&
                        s.Equals(val, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
