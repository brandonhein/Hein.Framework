using System.Text.RegularExpressions;

namespace Hein.Framework.Extensions
{
    public static class PhoneNumberExtensions
    {
        public static string ToPrettyPhone(this string phoneNumber, char characterBreak)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return string.Empty;

            if (characterBreak.IsOneOf('.', '-'))
            {
                phoneNumber = phoneNumber.ToPrettyPhone();
                phoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Trim();
                phoneNumber = phoneNumber.Replace('-', characterBreak).Replace(' ', characterBreak);
                return phoneNumber;
            }

            throw new System.Exception("Character Break isnt one of pretty phone options");
        }

        public static string ToPrettyPhone(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }

            try
            {
                phoneNumber = phoneNumber.ToCleanPhone();
                var phoneLength = phoneNumber.Length;

                long number = long.Parse(phoneNumber);
                string formattedValue = string.Empty;
                if (phoneLength == 10)
                {
                    formattedValue = number.ToString("###-###-####");
                }
                else if (phoneLength == 7)
                {
                    formattedValue = number.ToString("###-####");
                }
                else
                {
                    formattedValue = number.ToString();
                }

                return formattedValue;
            }
            catch
            { }

            return phoneNumber;
        }

        public static string ToPrettyPhone(this long dirtyPhone) => ToPrettyPhone((long?)dirtyPhone);

        public static string ToPrettyPhone(this long? dirtyPhone)
        {
            if (dirtyPhone.HasValue)
                return dirtyPhone.Value.ToString().ToPrettyPhone();

            return string.Empty;
        }

        public static string ToCleanPhone(this string dirtyPhone)
        {
            if (string.IsNullOrEmpty(dirtyPhone))
            {
                return null;
            }

            return CleanPhone(dirtyPhone);
        }

        public static string CleanPhone(string dirtyPhone)
        {
            if (string.IsNullOrEmpty(dirtyPhone))
                return dirtyPhone;

            dirtyPhone = dirtyPhone.Trim();

            if (dirtyPhone.StartsWith("+"))
                dirtyPhone = dirtyPhone.TrimStart('+');

            if (dirtyPhone.StartsWith("1") && dirtyPhone.Length > 10)
                dirtyPhone = dirtyPhone.Remove(0, 1);

            var phone = Regex.Replace(dirtyPhone, "[^0-9]", "");
            if (string.IsNullOrEmpty(phone))
            {
                return null;
            }

            if (phone.Length > 10)
            {
                return phone.Substring(0, 10);
            }

            return phone.SafeTrim();
        }
    }
}
