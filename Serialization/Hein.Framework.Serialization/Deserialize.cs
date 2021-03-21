using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Hein.Framework.Serialization
{
    public static class Deserialize
    {
        public static T FromJson<T>(this string json)
        {
            return FromJson<T>(json, SerializerSettings.DefaultOptions);
        }

        public static T FromJson<T>(this string json, params JsonConverter[] converters)
        {
            var options = SerializerSettings.DefaultOptions;
            foreach (var converter in converters)
            {
                options.Converters.Add(converter);
            }

            return FromJson<T>(json, options);
        }

        public static T FromJson<T>(this string json, JsonSerializerOptions options) => JsonSerializer.Deserialize<T>(json, options);

        public static T FromJson<T>(this Stream jsonStream)
        {
            return FromJson<T>(jsonStream, SerializerSettings.DefaultOptions);
        }

        public static T FromJson<T>(this Stream jsonStream, params JsonConverter[] converters)
        {
            var options = SerializerSettings.DefaultOptions;
            foreach (var converter in converters)
            {
                options.Converters.Add(converter);
            }

            return FromJson<T>(jsonStream, options);
        }

        public static T FromJson<T>(this Stream jsonStream, JsonSerializerOptions options)
        {
            try
            {
                return JsonSerializer.DeserializeAsync<T>(jsonStream, options).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Obsolete("Use FromJson extension as this will be depericated in later releases")]
        public static T JsonToObject<T>(this string json)
        {
            return FromJson<T>(json);
        }


        public static T FromXml<T>(this string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            xml = XmlUtilities.ReplaceBadValues(xml);
            xml = xml.CompressXml()
                .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", string.Empty);

            var encoding = new UTF8Encoding();
            using (var memoryStream = new MemoryStream())
            {
                byte[] buffer = encoding.GetBytes(xml);
                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Position = 0;

                return FromXml<T>(memoryStream);
            }
        }

        public static T FromXml<T>(this Stream xmlStream)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var xmlTextWriter = new XmlTextWriter(xmlStream, Encoding.UTF8))
            {
                return (T)serializer.Deserialize(xmlStream);
            }
        }

        [Obsolete("Use FromXml extension as this will be depericated in later releases")]
        public static T XmlToObject<T>(this string xml)
        {
            return FromXml<T>(xml);
        }

        public static T FromSoapXml<T>(this string soapXml)
        {
            var soapBodyXml = SoapUtilities.StripSoapThings(soapXml);
            return FromXml<T>(soapBodyXml);
        }

        [Obsolete("Use FromSoapXml extension as this will be depericated in later releases")]
        public static T SoapToObject<T>(this string soapXml)
        {
            return FromSoapXml<T>(soapXml);
        }
    }
}
