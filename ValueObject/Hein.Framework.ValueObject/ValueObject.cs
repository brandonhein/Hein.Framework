using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.ValueObject
{
    /// <summary>
    /// Using a Value Object (also referenced as Value Type), you can compare objects by the value's they contain and not by their identity.
    /// <para>Theres lots of documentation on Value Objects</para>
    /// <para>Source: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects </para>
    /// <para>Source: https://enterprisecraftsmanship.com/posts/value-objects-explained/ </para>
    /// <para>Source: https://enterprisecraftsmanship.com/posts/value-object-better-implementation/ </para>
    /// </summary>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var valueObject = (ValueObject<T>)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
