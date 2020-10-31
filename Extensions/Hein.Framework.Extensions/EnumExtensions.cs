using System;

namespace Hein.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum", str);
            }
            return (T)Enum.Parse(typeof(T), str, ignoreCase: true);
        }
    }
}
