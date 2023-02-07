namespace LinqBasics;

class Entry
{
    public static void Main(string[] args)
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        IEnumerable<int> filtered = numbers.Where(i => i > 4);
        List<int> filteredList = numbers.Where(i => i > 4).ToList();
        IEnumerable<int> squared = numbers.Select(i => i * i);

        // Before numbers was updated
        System.Console.WriteLine("IEnumerable, before source List was updated:");
        foreach( var item in filtered)
            System.Console.WriteLine(item);
        // Adding a number to numbers
        numbers.Add(10);
        System.Console.WriteLine("IEnumerable, after source List was updated:");
        // Appended number 10 is included while accessing filtered, due to deferred execution
        foreach( var item in filtered)
            System.Console.WriteLine(item);
        // Notice how when IEnumerable is converted to a List, the results are finalised
        System.Console.WriteLine("Filtered List, after source List was updated:");
        foreach( var item in filteredList)
            System.Console.WriteLine(item);

    }

    // Linq basically operates on IEnumerables, which basically look as follows.
    // You don't add stuff to the sequence, but you mutate the data received from it.
    // Objects returned by Linq are always IEnumerable<T>.
    interface IMyEnumerator<T>
    {
        T Current { get; }
        bool MoveNext();
    }
    interface IMyEnumerable<T>
    {
        IMyEnumerator<T> GetEnumerator();
    }
}