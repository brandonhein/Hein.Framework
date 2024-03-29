﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hein.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                var textBytes = Encoding.UTF8.GetBytes(val);
                return Convert.ToBase64String(textBytes);
            }
            else
            {
                return null;
            }
        }

        public static string FromBase64(this string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                var textBytes = Convert.FromBase64String(val);
                return Encoding.UTF8.GetString(textBytes);
            }
            else
            {
                return null;
            }
        }

        public static string Between(this string strSource, string strStart, string strEnd)
        {
            if (string.IsNullOrEmpty(strSource))
            {
                return string.Empty;
            }

            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static Stream ToStream(this string s) => GenerateStream(s);

        public static Stream GenerateStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

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

        public static bool IsOneOf(this string val, IEnumerable<string> comparisonValue)
        {
            return IsOneOf(val, comparisonValue.ToArray());
        }

        public static string StandardizeForCompare(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.ToUpper().Trim();
            }
            return value;
        }

        public static string StripPuncuation(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return new string(value.Where(x => !char.IsPunctuation(x)).Where(x => !char.IsSymbol(x)).ToArray());
        }

        public static bool ContainsCaseInsensitive(this string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) != -1;
        }

        public static string SafeTrim(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            try
            {
                return value.Trim();
            }
            catch
            {
                return value;
            }
        }

        public static string ToCapitalize(this string str) => Capitalize(str);

        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            str = str.ToLower();

            char[] array = str.ToCharArray();
            if (array.Length >= 1 && char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }

            for (int i = 1; i < array.Length; i++)
            {
                if ((array[i - 1] == ' ' || array[i - 1] == '-') && char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
            return new string(array);
        }
    }
}
