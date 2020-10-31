using System;

namespace Hein.Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static bool CanConvertTo<T>(this object obj)
        {
            try
            {
                var result = (T)Convert.ChangeType(obj, typeof(T));
                return !result.Equals(default(T));
            }
            catch
            {
                return false;
            }
        }

        public static short ToInt16(this object obj)
        {
            return obj != null && obj.CanConvertTo<int>() ? Convert.ToInt16(obj) : default(short);
        }

        public static int ToInt32(this object obj)
        {
            return obj != null && obj.CanConvertTo<int>() ? Convert.ToInt32(obj) : default(int);
        }

        public static long ToInt64(this object obj)
        {
            return obj != null && obj.CanConvertTo<long>() ? Convert.ToInt64(obj) : default(long);
        }

        public static bool ToBoolean(this object obj)
        {
            return obj != null && obj.CanConvertTo<bool>() ? Convert.ToBoolean(obj) : default(bool);
        }
    }
}
