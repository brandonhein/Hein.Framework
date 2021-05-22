## Hein.Framework.ValueObject
Value Object (also known as Value Type) is a design pattern that also developers to compare domain objects by the value(s) they contain rather by their identity.

## Scenario

Say I have a `Redbull` class/entity. It'll contain an `Id` (because it's saved in a db), `Name` for the kind of type/flavor, `Year` and `Price`.

In this scenario, my application needs to handle the `2021 Summer Edition` differently, as it's seasonal, and this years batch is a new forumla.  How do you do this?

- Price can and will fluctuate, so it's meaningless in this example in our handling.
- Because we have a multi-environment (Dev,Test,Prod), You can't just hardcode a value for `Id`, and coding a comparison/lookup by `Id` would be an unnecessary hit to the database (especially if this logic is only to be done 4 months of the year).

Now you can tell me, '[brandonhein](https://github.com/brandonhein), we can just do string comparisons on the `Name` and `Year` properties to achieve this, duh'.  And with that, I say 'Well, yeah you can, but then you might liter your codebase with string/year comparisons, and thats ugly. Good luck trying to maintain that and grow!'

This is why Value Objects can help.  Let me show.

## Example

My `Redbull.cs` class (just a regular ol' Entity):
```csharp
public class Redbull
{
   [Key]
   public Guid Id { get; set; }
   public string Name { get; set; }
   public int Year { get; set; }
   public decimal Price { get; set; }
}
```

Now, when I add the `ValueObject<Redbull>` as a base class, I can implement the `GetEqualityComponents` method, and do object comparisons on specific properites instead of all of them
```csharp
public class Redbull : ValueObject<Redbull>
{
   [Key]
   public Guid Id { get; set; }
   public string Name { get; set; }
   public int Year { get; set; }
   public decimal Price { get; set; }

   protected override IEnumerable<object> GetEqualityComponents()
   {
      // Using a yield return statement to return each element one at a time
      yield return Name;
      yield return Year;
   }
}
```

With this in place, I could have `Redbull` object/class with those 2 values
```csharp
var summerEdition2021 = new Redbull { Name = "Summer Edition", Year = 2021 };
```
OR you can add a `static` property in the `Redbull` class, to show there's value objects used for this entity
```csharp
public class Redbull : ValueObject<Redbull>
{
   //commented properites and GetEqualityComponents for space

   public static Redbull SummerEdition2021 => new Redbull { Name = "Summer Edition", Year = 2021 };
}
```

Now that our `Redbull` value object is setup, we can use the value object comparisons to our advantage for cleaner comparison
```csharp
var redbullsOrdered = new Redbull[] { new Redbull { Name = "Regular", Year = 2021 }, new Redbull { Name = "Summer Edition", Year = 2021 } };

foreach (var redbull in redbullsOrdered)
{
   if (redbull == Redbull.SummerEdition2021) //<--- look how clean this looks!
   {
      DoRedbullSummerEdition2021Things();
   }
}
```