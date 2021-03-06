﻿using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Hein.Framework.Serialization
{
    public static class Serialize
    {
        public static string ToXml(Type type, object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            var xmlserializer = new XmlSerializer(type);
            var stringWriter = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var resultXml = string.Empty;
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, obj, ns);
                resultXml = stringWriter.ToString();
            }

            resultXml = XmlUtilities.CleanXmlNamespaces(resultXml);
            return resultXml;
        }

        public static string ToXml<T>(this T obj)
        {
            return ToXml(typeof(T), obj);
        }

        /// <summary>
        /// Converts any object to json string using default settings
        /// </summary>
        public static string ToJson(this object obj)
        {
            return ToJson(obj, SerializerSettings.DefaultOptions);
        }

        /// <summary>
        /// Converts any object to json string using default settings, while also adding additional custom converters you might have
        /// </summary>
        public static string ToJson(this object obj, params JsonConverter[] converters)
        {
            var options = SerializerSettings.DefaultOptions;
            foreach (var converter in converters)
            {
                if (!options.Converters.Contains(converter))
                    options.Converters.Add(converter);
            }

            return ToJson(obj, options);
        }

        /// <summary>
        /// Converts any object to json string using any <see cref="JsonSerializerOptions"/> you want
        /// </summary>
        public static string ToJson(this object obj, JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        public static string ToSoapXml<T>(this T value)
        {
            var xml = value.ToXml();
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty)
                .Replace("<?xml version=\"1.0\" encoding=\"UTF-16\"?>", string.Empty)
                .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", string.Empty)
                .Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", string.Empty)
                .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", string.Empty)
                .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty)
                .Replace("\\", string.Empty);

            var soap = SoapUtilities.GenerateSoap(xml);
            return soap;
        }
    }
}
