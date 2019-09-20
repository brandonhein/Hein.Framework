using System;
using System.Runtime.Serialization;

namespace Hein.Framework.DependencyInjection
{
    [Serializable]
    public class ServiceLocatorNotBuiltException : Exception
    {
        public ServiceLocatorNotBuiltException() : base("BuildServiceLocator method was not called so the it does not know about your service you want")
        { }

        protected ServiceLocatorNotBuiltException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
