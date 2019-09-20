using Amazon.DynamoDBv2.DataModel;
using Hein.Framework.Dynamo.Entity;
using System;

namespace Hein.Framework.Dynamo.Tests.Helpers
{
    [DynamoDBTable("HelloPerson")]
    public class Person : IEntity, ISoftDelete
    {
        [Key]
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAwesome { get; set; }
        public bool IsDeleted { get; set; }

        [Exclude]
        public bool IsFirstNameBrandon
        {
            get { return this.FirstName == "Brandon"; }
        }

        public void ExecuteAfterGet()
        {
            Console.WriteLine("Executing After 'Person' Get");
        }

        public void ExecuteAfterSave()
        {
            Console.WriteLine("Executing After 'Person' Save");
        }

        public void ExecuteBeforeSave()
        {
            Console.WriteLine("Executing Before 'Person' Save");
        }

        public Guid GetId()
        {
            return Guid.Parse(this.PersonId);
        }

        public void SetId(Guid id)
        {
            this.PersonId = id.ToString();
        }
    }
}
