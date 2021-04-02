//using System;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//
//namespace Hein.Framework.Serialization.Converters
//{
//    public class ExceptionConverter : JsonConverter<Exception>
//    {
//        public override bool CanConvert(Type typeToConvert) => true;// typeToConvert == typeof(Exception);
//
//        public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            return null;
//        }
//
//        public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
//        {
//            writer.WriteStartObject();
//            writer.WriteString("Message", value.Message);
//            writer.WriteString("Source", value.Source);
//            if (value.Data != null && value.Data.Count > 0)
//            {
//                writer.WritePropertyName("Data");
//                writer.WriteStartObject();
//                foreach (var key in value.Data.Keys)
//                {
//                    writer.WriteString(key.ToString(), value.Data[key].ToString());
//                }
//                writer.WriteEndObject();
//            }
//
//            if (value.InnerException != null)
//            {
//                writer.WritePropertyName("InnerException");
//                new ExceptionConverter().Write(writer, value.InnerException, options);
//            }
//
//            writer.WriteString("Stack", value.StackTrace);
//
//            writer.WriteEndObject();
//        }
//    }
//}
//