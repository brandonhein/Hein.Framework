using System;
using System.Collections.Generic;
using Xunit;

namespace Hein.Framework.ValueObject.Tests
{
    public class EqualityTests
    {
        [Fact]
        public void Should_show_equal_or_not_by_redbull_flavor_id_and_price_has_no_significance_because_its_not_in_equality_components()
        {
            Assert.Equal(Redbull.Watermelon, Redbull.Watermelon);
            Assert.NotEqual(Redbull.SugarFree, Redbull.Regular);

            Assert.Equal(new Redbull() { Flavor = "Coconut Berry", Price = 2.25m }, new Redbull() { Flavor = "Coconut Berry", Price = 2.30m });
        }
    }

    public class Redbull : ValueObject<Redbull>
    {
        public Redbull() => Id = Guid.NewGuid();

        public Guid Id { get; }
        public string Flavor { get; set; }
        public decimal Price { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Flavor;
        }

        public static Redbull Regular => new Redbull() { Flavor = "Regular", Price = 2.50m };
        public static Redbull SugarFree => new Redbull() { Flavor = "SugarFree", Price = 2.30m };
        public static Redbull Watermelon => new Redbull() { Flavor = "Watermelon", Price = 2.25m };
    }
}
