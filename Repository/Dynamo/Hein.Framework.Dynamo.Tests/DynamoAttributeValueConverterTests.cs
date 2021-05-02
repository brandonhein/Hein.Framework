using Hein.Framework.Dynamo.Converters;
using Hein.Framework.Dynamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hein.Framework.Dynamo.Tests
{
    public class DynamoAttributeValueConverterTests
    {
        [Fact]
        public void Should_find_and_convert_to_dynamo_attribute_value()
        {
            var table = new Table();
            table.Add(new Item());

            var result = table.MapToDynamo();
            var tableAgain = result.MapFromDynamo<Table>();

            Assert.Equal(table.Value, tableAgain.Value);
            Assert.Equal(table.Numbers, tableAgain.Numbers);
            Assert.Equal(table.MoarItems.Count(), tableAgain.MoarItems.Count());
            Assert.Equal(table.Items.Count(), tableAgain.Items.Count());
        }
    }

    public class Table
    {
        public Table()
        {
            Value = "hiya";
            Numbers = new int[] { 16, 74, 78 };
            Items = new Item[] { };
            MoarItems = new Item2[] { new Item2(), new Item2() };
        }

        public void Add(Item item)
        {
            var items = Items.ToList();
            items.Add(item);
            Items = items.ToArray();
        }

        public string Value { get; set; }
        public int[] Numbers { get; set; }
        public IEnumerable<Item2> MoarItems { get; set; }
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class Item2
    {
        public Item2() => AnotherId = Guid.NewGuid().ToString();
        public string AnotherId { get; set; }
    }
}
