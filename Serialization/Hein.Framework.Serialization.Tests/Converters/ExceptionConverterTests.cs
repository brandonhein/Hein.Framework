//using Hein.Framework.Serialization.Converters;
//using System.Net;
//using System.Net.Mail;
//using Xunit;
//
//namespace Hein.Framework.Serialization.Tests.Converters
//{
//    public class ExceptionConverterTests
//    {
//        [Fact]
//        public void Should_serialize_exceptions_and_an_okay_fashion()
//        {
//            var innerException = new WebException("message", WebExceptionStatus.ProtocolError);
//            var exception = new SmtpException("ope", innerException);
//            var json = exception.ToJson(new ExceptionConverter());
//        }
//    }
//}
