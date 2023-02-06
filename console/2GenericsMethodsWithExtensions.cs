namespace GenericsMethodsWithExtensions;

class Entry
{
    public static void Main(string[] args)
    {

        var people = new List<Person> { new Person("Aaron", "L1"), new Person("Andrew", "L2"), new Person("Nelson", "L3") };
        var firstNames = people
            .Filter(i => i.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase))
            .Map(p => p.FirstName);

        foreach(var name in firstNames) {
            System.Console.WriteLine(name);
        }
    }


}

static class EnumerableExtensions {
    public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> that, Func<TSource, TResult> projection) {
        foreach(var item in that)
            yield return projection(item);
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> that, Func<T, bool> predicate)
    {
        foreach (var item in that)
            if (predicate(item))
                // yield and IEnumerable make this an iterator block
                // This results in DEFERRED EXECUTION
                yield return item;
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