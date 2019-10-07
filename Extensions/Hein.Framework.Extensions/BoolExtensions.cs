namespace Hein.Framework.Extensions
{
    public static class BoolExtensions
    {
        public static string ToYesNo(this bool val)
        {
            string retVal = string.Empty;

            retVal = val ? "Yes" : "No";

            return retVal;
        }

        public static string ToYN(this bool val)
        {
            string retVal = string.Empty;
            retVal = val ? "Y" : "N";
            return retVal;
        }

        public static string ToTF(this bool val)
        {
            return val ? "T" : "F";
        }

        public static int ToOneZero(this bool val)
        {
            return val ? 1 : 0;
        }

        public static bool ToBoolean(this string val)
        {
            return !string.IsNullOrEmpty(val) && val.ToUpper().IsOneOf("TRUE", "T", "Y", "YES", "1");
        }

        public static bool ToBoolean(this int val)
        {
            return val == 1;
        }
    }
}
