## Hein.Framework.Dynamo (For AWS DynamoDB database usage)
Inspiration: Getting involved with Cloud Development, I wanted to challenge myself to see if I can find a way to take the `Hein.Framework.Repository` library, and map an equivalency to a DynamoDB(NoSQL).  With some help from [marcodafonseca's dynamo.orm repo]( https://github.com/marcodafonseca/Dynamo.ORM) I was able to understand the DynamoDB requests and create a `QueryOver` usage.  Uses a unit of work structure to track your updates... and at either at the Dispose or Commit, will secure the Database calls.  The more dynamic your query, the more chances you will run into problems.  Keep your queries simple and dynamo will thank you!   

Sample #1 - Basic Entity/Dynamo Table Select
```csharp
//RepositoryContext takes in a Amazon.RegionEndpoint as it builds a AmazonDynamoDBConfig with it
using (var context = new RepositoryContext(RegionEndpoint.USEast2))
{
  //same set up like Hein.Framework.Repository... pass in the current context
  var repo = new EntityRepository<Person>(context);
  
  //queryover to build a 'scan' request using your Entity/Table 
  var query = QueryOver.Of<Person>()
      .Where(x => x.FirstName == "Sonic")
      .Top(5);
      
  //queryover... and mapped to an IEnumerable<Person> result
  var persons = repo.Find(query);
}
```

Sample #2 - Entity/Dynamo Item saving is easy!
```csharp
using (var context = new RepositoryContext(RegionEndpoint.USEast2))
{
  try
  {
    var repo = new EntityRepository<Person>(context);
    var query = QueryOver.Of<Person>().Where(x => x.PersonId == 16);
    var person = repo.Find(query).FirstOrDefault();
    
    person.FirstName = "Brandon";
    person.IsAwesome = true;
    
    //since PersonId is flagged on this entity as Key... and found in the db... 
    //Update will be called automatically
    repo.Save(person);
    
    var newPerson = new Person();
    newPerson.FirstName = "ImNew";
    newPerson.IsAwesome = false;
    
    //similar case... PersonId is null here
    //Insert will be called automatically
    repo.Save(newPerson);
    
    //if we wanted to delete a person we can call the Delete method in the repository
    repo.Delete(person);
    
    //commit db changes to the database to secure the 'saves'
    context.Commit();
  }
  catch (Exception ex)
  {
    //since an error occured... and depends where the error happend
    //rollback any/all changes inside this context
    context.Rollback();
  }
}
```