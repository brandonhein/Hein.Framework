using System;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Attributes
{
    /// <summary>
    /// Orders a property to be in a specific order when serailizing
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class JsonPropertyOrderAttribute : JsonAttribute
    {
        public JsonPropertyOrderAttribute(int order)
        {
            Order = order;
        }
    
        public int Order { get; }
    }
}
