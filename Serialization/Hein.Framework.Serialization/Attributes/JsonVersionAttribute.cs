using System;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Attributes
{
    /// <summary>
    /// Adds a property at the top of the json object signifying a specific version of an object on serialization
    /// <para>Prefect for message/event versioning</para>
    /// <para>Property name is: 'Version'</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class JsonVersionAttribute : JsonAttribute
    {
        public JsonVersionAttribute(string version)
            : this("Version", version)
        { }

        public JsonVersionAttribute(int version)
            : this("Version", version)
        { }

        public JsonVersionAttribute(double version)
            : this("Version", version)
        { }

        public JsonVersionAttribute(string propertyName, int version)
        {
            Name = propertyName;
            Version = version.ToString();
            IsNumber = true;
        }

        public JsonVersionAttribute(string propertyName, double version)
        {
            Name = propertyName;
            Version = version.ToString();
            IsNumber = true;
        }

        public JsonVersionAttribute(string propertyName, string version)
        {
            Name = propertyName;
            Version = version;
            IsNumber = false;
        }

        public string Name { get; }
        public bool IsNumber { get; }
        public string Version { get; }
    }

    /// <summary>
    /// Adds a property at the top of the json object signifying a specific version of an object on serialization
    /// <para>Prefect for message/event versioning</para>
    /// <para>Property name is: 'Version'</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class VersionAttribute : JsonVersionAttribute
    {
        public VersionAttribute(string version)
            : base("Version", version)
        { }

        public VersionAttribute(int version)
            : base("Version", version)
        { }

        public VersionAttribute(double version)
            : base("Version", version)
        { }
    }

    /// <summary>
    /// Adds a property at the top of the json object signifying a specific version of an object on serialization
    /// <para>Prefect for message/event versioning</para>
    /// <para>Property name is defaulted to: 'EventVersion'</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class EventVersionAttribute : JsonVersionAttribute
    {
        public EventVersionAttribute(string version)
            : base("EventVersion", version)
        { }

        public EventVersionAttribute(int version)
            : base("EventVersion", version)
        { }

        public EventVersionAttribute(double version)
            : base("EventVersion", version)
        { }
    }

    /// <summary>
    /// Adds a property in a json object signifying a specific version of an object
    /// <para>Prefect for message/event versioning</para>
    /// <para>Property name is: 'SpecVersion'</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class SpecVersionAttribute : JsonVersionAttribute
    {
        public SpecVersionAttribute(string version)
            : base("SpecVersion", version)
        { }

        public SpecVersionAttribute(int version)
            : base("SpecVersion", version)
        { }

        public SpecVersionAttribute(double version)
            : base("SpecVersion", version)
        { }
    }
}
