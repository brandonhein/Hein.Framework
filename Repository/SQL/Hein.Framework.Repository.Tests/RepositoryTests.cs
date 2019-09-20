using Hein.Framework.Repository.Criterion;
using Hein.Framework.Repository.Entity;
using Hein.Framework.Repository.Tests.Helpers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hein.Framework.Repository.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void Should_create_new_person_id_record_for_repository()
        {
            var mockContext = new Mock<IRepositoryContext>();
            mockContext.Setup(x => x.Query<long>(It.IsAny<string>())).Returns(new List<long>() { 16 });

            var repository = new EntityRepository<PersonEntity>(mockContext.Object);

            var person = new PersonEntity();
            person.FirstName = "Red";
            person.LastName = "Bull";
            person.IsAwesome = true;

            //no id assigned to this entity yet
            Assert.True(person.PersonId == 0);

            repository.Save(person);

            //save happened... sooooo id!!!!
            Assert.True(person.PersonId == 16);

            mockContext.Verify(x => x.Query<long>(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Should_update_person_id_record_for_repository()
        {
            var mockContext = new Mock<IRepositoryContext>();
            mockContext.Setup(x => x.Execute(It.IsAny<string>()));

            var repository = new EntityRepository<PersonEntity>(mockContext.Object);

            var person = new PersonEntity();
            person.SetId(16);
            person.FirstName = "Red";
            person.LastName = "Bull";
            person.IsAwesome = true;

            //call save to verify 'update' was called
            repository.Save(person);

            mockContext.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void Should_delete_person_id_record_for_repository()
        {
            var person = new PersonEntity();
            person.SetId(16);
            person.FirstName = "Red";
            person.LastName = "Bull";
            person.IsAwesome = true;

            var mockContext = new Mock<IRepositoryContext>();
            mockContext.Setup(x => x.Query<PersonEntity>(It.IsAny<string>())).Returns(new List<PersonEntity>() { person });
            mockContext.Setup(x => x.Execute(It.IsAny<string>()));

            var repository = new EntityRepository<PersonEntity>(mockContext.Object);

            var query = QueryOver.Of<PersonEntity>()
                .Where(x => x.PersonId == 16);

            var result = repository.Find(query).FirstOrDefault();

            Assert.True(result.PersonId == person.PersonId);
            Assert.True(result.FirstName == person.FirstName);
            Assert.True(result.LastName == person.LastName);
            Assert.True(result.IsAwesome == person.IsAwesome);

            //not deleted yet
            Assert.True(result.IsDeleted == false);

            //call to delete
            repository.Delete(result);

            //flagged as deleted... yay!!!
            Assert.True(result.IsDeleted == true);

            mockContext.Verify(x => x.Query<PersonEntity>(It.IsAny<string>()), Times.Once);
            mockContext.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
        }
    }
}
