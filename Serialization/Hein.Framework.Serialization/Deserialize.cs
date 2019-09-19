using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hein.Framework.Serialization
{
    public static class Deserialize
    {
        public static T JsonToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T XmlToObject<T>(this string xml)
        {
            var type = typeof(T);
            xml = XmlUtilities.ReplaceBadValues(xml);
            xml = xml.CompressXml()
                .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", string.Empty);

            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xml");
            }

            try
            {
                var serializer = new XmlSerializer(type);
                var encoding = new UTF8Encoding();
                using (var memoryStream = new MemoryStream())
                {
                    byte[] buffer = encoding.GetBytes(xml);
                    memoryStream.Write(buffer, 0, buffer.Length);
                    memoryStream.Position = 0;

                    using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
                    {
                        return (T)serializer.Deserialize(memoryStream);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Error deserializing object", ex);
            }
        }

        public static T SoapToObject<T>(this string soapXml)
        {
            var soapBodyXml = SoapUtilities.StripSoapThings(soapXml);
            return XmlToObject<T>(soapBodyXml);
        }
    }
}
