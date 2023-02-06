namespace GenericsMethodsWithLinq;

/**
* About Linq
* 1. A Bunch of Extension Methods on the IEnumerable type
* 2. A Language Extension that works with those Extension Methods
* 3. A few Static Methods that allow you Range, Repeat, Empty, etc
* 4. IQueryable - Almost exactly like IEnumerable. 
*       An aspect of Linq that allows you to work with Expressions instead of Delegates.
*       e.g. passing expressions to an ORM
**/
class Entry
{
    public static void Main(string[] args)
    {

        var people = new List<Person> { new Person("Aaron", "L1"), new Person("Andrew", "L2"), new Person("Nelson", "L3") };
        var firstNames = people
        // Linq is pretty much where you will look initially for performing such operations on data / resultsets / Enumerables
            .Where(i => i.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase))
            .Select(p => p.FirstName);

        foreach (var name in firstNames)
        {
            System.Console.WriteLine(name);
        }

        // Linq is also a language extension to C#
        var firstNames2 =
            from p in people
            where p.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase)
            //orderby p.LastName
            select p.FirstName;

        foreach (var name in firstNames2)
        {
            System.Console.WriteLine(name);
        }

        // Other Linq uses
        var numbers = Enumerable.Range(0, 100).Select(i => i * i).Where(i => i < 50);
        foreach (var n in numbers)
        {
            System.Console.WriteLine(n);
        }

        // Linq does not mutate the source data. e.g. 
        // Here Linq does not mutate any of the below arrays. 
        var numbers2 = new[] { 1, 2, 3, 4 }.Concat(new[] { 5, 6, 7, 8, 9 });

        // Even the foreach loop gets compiled down to IEnumerable.GetEnumerator() 
        // and then iterating over the elements.

    }


}

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}