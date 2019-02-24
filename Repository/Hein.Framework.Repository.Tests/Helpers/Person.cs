using Hein.Framework.Repository.Entity;
using System;

namespace Hein.Framework.Repository.Tests.Helpers
{
    public class Person : IEntity, ISoftDelete
    {
        [Key]
        public long PersonId { get; private set; }
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

        public long GetId()
        {
            return this.PersonId;
        }

        public void SetId(long id)
        {
            this.PersonId = id;
        }
    }
}
