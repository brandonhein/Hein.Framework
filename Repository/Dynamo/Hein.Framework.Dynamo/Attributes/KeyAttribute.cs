using System;

namespace Hein.Framework.Dynamo.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class KeyAttribute : Attribute
    { }
}
