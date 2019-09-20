using System.Text;

namespace Hein.Framework.Serialization
{
    public static class SoapUtilities
    {
        public static string StripSoapThings(this string val)
        {
            return val.Replace("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">", string.Empty)
                .Replace("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">", string.Empty)
                .Replace("i:nil=\"true\"", string.Empty)
                .Replace("<soap:Body>", string.Empty)
                .Replace("</soap:Envelope>", string.Empty)
                .Replace("</soap:Body>", string.Empty)
                .Replace("\\", string.Empty)
                .CompressXml();
        }

        public static string SoapTemplate
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                    .Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">")
                    .Append("<soap:Body>")
                    .Append("{{BODY}}")
                    .Append("</soap:Body>")
                    .Append("</soap:Envelope>");

                return builder.ToString();
            }
        }

        public static string GenerateSoap(string bodyXmlForSoap)
        {
            return SoapTemplate.Replace("{{BODY}}", bodyXmlForSoap);
        }
    }
}
