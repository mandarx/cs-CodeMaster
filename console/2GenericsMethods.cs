namespace GenericsMethods;

class Entry
{
    public static void Main(string[] args)
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        var odd = Filter<int>(list, i => i % 2 != 0);
        var even = Filter(list, i => i % 2 == 0); // Though C# is statically typed, it is very good at inferring <int>

        System.Console.WriteLine("ODD:");
        foreach (var item in odd)
            System.Console.WriteLine(item);

        System.Console.WriteLine("EVEN:");
        foreach (var item in even)
            System.Console.WriteLine(item);

        /*-----------------------------------*/

        var people = new List<Person> { new Person("Aaron", "L1"), new Person("Andrew", "L2"), new Person("Nelson", "L3") };
        // Filter all people whose name starts with the letter 'a' (Case Insensitive)

        // Traditional solution
        var peopleStatringWithA = new List<Person>();
        foreach (var person in people)
            if (person.FirstName.Length > 0 && person.FirstName.ToLower()[0] == 'a')
                peopleStatringWithA.Add(person);
        
        System.Console.WriteLine("People Startswith Traditional:");
        peopleStatringWithA.ForEach(p => System.Console.WriteLine($"{p.FirstName} {p.LastName}"));

        // Using Generics + Delegates + Lambda 
        var peopleStatringWithA2 = Filter(people, p => p.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase));
        System.Console.WriteLine("People Startswith Modern:");
        peopleStatringWithA2.ToList().ForEach(p => System.Console.WriteLine($"{p.FirstName} {p.LastName}"));

        // Use Map method and see deferred execution (by debugging this code)
        IEnumerable<String> firtNames   = Map<Person, string> (peopleStatringWithA2, p => p.FirstName); // Explicit
        var                 firstNames2 = Map                 (peopleStatringWithA2, p => p.FirstName); // Inferred

        foreach(var name in firstNames2) {
            System.Console.WriteLine(name);
        }
    }

    static IEnumerable<TResult> Map<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> projection) {
        foreach(var item in source)
            yield return projection(item);
    }

    static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                // yield and IEnumerable make this an iterator block
                // This results in DEFERRED EXECUTION
                yield return item;
    }


    /* Following does not work because we do not know their type
    static T Hey<T> () {
        return new T();
    }
    */

    // The following will work, but is not very useful.
    static T Hey<T>() where T : new()
    {
        return new T();
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