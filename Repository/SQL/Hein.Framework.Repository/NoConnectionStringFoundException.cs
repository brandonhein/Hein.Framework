using System;
using System.Runtime.Serialization;

namespace Hein.Framework.Repository
{
    [Serializable]
    public class NoConnectionStringFoundException : Exception
    {
        public NoConnectionStringFoundException() : base("No Connection String Found in IConfiguration")
        { }

        protected NoConnectionStringFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
