using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Hein.Framework.Serialization
{
    public static class XmlUtilities
    {
        public static string CleanXmlNamespaces(string xml)
        {
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xml = xml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            xml = xml.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            xml = xml.Replace(" xsi:nil=\"true\"", "");
            xml = xml.Replace("\0", "");
            xml = xml.CompressXml();

            return xml;
        }

        public static string ToIndentedXml(this string xml)
        {
            return FormatXml(xml);
        }

        public static string FormatXml(string unformattedXml)
        {
            var xd = new XmlDocument();
            xd.LoadXml(unformattedXml);
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var xtw = new XmlTextWriter(sw))
                {
                    xtw.Formatting = Formatting.Indented;
                    xd.WriteTo(xtw);
                }
            }
            return sb.ToString();
        }

        public static string CompressXml(this string value)
        {
            value = value.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", "")
                .Replace(Environment.NewLine, "");

            var doc = new XmlDocument();
            doc.LoadXml(value);
            return doc.OuterXml;
        }

        public static string RemoveControlCharacters(string inString)
        {
            if (inString == null)
            {
                return null;
            }

            var newString = new StringBuilder();
            char ch;
            for (int i = 0; i < inString.Length; i++)
            {
                ch = inString[i];
                if (!char.IsControl(ch))
                {
                    newString.Append(ch);
                }
            }
            return newString.ToString();
        }

        public static string RemoveTroublesomeCharacters(string inString)
        {
            if (inString == null) return null;

            var newString = new StringBuilder();

            for (int i = 0; i < inString.Length; i++)
            {
                char ch = inString[i];

                if ((ch < 0x00FD && ch > 0x001F) || ch == '\t' || ch == '\n' || ch == '\r')
                {
                    newString.Append(ch);
                }
            }
            return newString.ToString();
        }

        public static string ReplaceBadValues(string str)
        {
            return str.Replace("&", "and");
        }
    }
}
