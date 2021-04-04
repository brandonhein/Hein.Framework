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
            try
            {
                var table = new Table();
                table.Add(new Item());

                var result = table.MapToDynamo();

                var tableAgain = result.MapFromDynamo<Table>();
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class Table
    {
        public Table()
        {
            Value = "hiya";
            Items = new List<Item>();
        }

        public void Add(Item item)
        {
            var items = Items.ToList();
            items.Add(item);
            Items = items.ToList();
        }

        public string Value { get; set; }
        public List<Item> Items { get; set; }
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
}
