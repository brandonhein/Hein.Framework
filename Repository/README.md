[Click here for: [SQL]](https://github.com/brandonhein/Hein.Framework/tree/master/Repository#heinframeworkrepository-for-sql-database-usage)  
[Click here for: [Dynamo]](https://github.com/brandonhein/Hein.Framework/tree/master/Repository#heinframeworkdynamo-for-aws-dynamodb-database-usage)

---

## Hein.Framework.Repository (For SQL database usage)
Inspiration: I've worked with a few other Object-Relational Mapping tools, like NHibernate, Entity Framework (EF), and Dapper.  Theres a few things that stood out to me that I enjoyed using.  Features like QueryOver, LINQ to SQL, and Query Result to Object mapping.  I wanted to build out a simple to use library that will showcase the things I liked applied to an application.

Sample #1 - Basic Entity Select Statement
```csharp
using (var context = new RepositoryContext())
{
  //use the db session context for the repo
  var repo = new EntityRepository<Person>(context);
  
  //queryover to build the query
  var query = QueryOver.Of<Person>()
      .Where(x => x.FirstName == "Brandon")
      .OrderByDesending(o => o.PersonId)
      .Top(5)
      .Distinct();
      
  //query is passed in... and mapped to an IEnumerable<Person> result
  var persons = repo.Find(query);
}
//disposing will auto call a commit if any queries were used
```
Sample #2 - Custom Repository (you can string querys to generate/map an bigger object that might use mulitple entities, elimitating multiple db calls)
```csharp
using (var context = new RepositoryContext())
{
  //this could use a builder method to keep track of what kind of query you want to include/build
  var query = new PersonQueryCriteria(16)
      .IncludePhoneContacts()
      .IncludeEmailContacts()
      .IncludeShoppingHistory();
      
  var repo = new PersonRepository();
  
  //you can pass in the context if the repository has an ICustomRepository interface
  repo.SetContext(context);
  
  //your custom repo can return a bigger object with one call and more details rather than 
  //small entities and multiple db calls
  var personObject = repo.Find(query);
}
```
Sample #3 - Entity Saving is a breeze!
```csharp
using (var context = new RepositoryContext())
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
---

## Hein.Framework.Dynamo (For AWS DynamoDB database usage)
Inspiration: Getting involved with Cloud Development, I wanted to challenge myself to see if I can find a way to take the `Hein.Framework.Repository` library, and map an equivalency to a DynamoDB(NoSQL).  With some help from [marcodafonseca's dynamo.orm repo]( https://github.com/marcodafonseca/Dynamo.ORM) I was able to understand the DynamoDB requests and create a `QueryOver` usage.  Uses a unit of work structure to track your updates... and at either at the Dispose or Commit, will secure the Database calls.   

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

