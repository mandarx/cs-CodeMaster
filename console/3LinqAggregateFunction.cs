namespace LinqAggregateFunction;
using LinqWithExtensionMethods;

class Entry
{
    class Product { }
    class Inventory
    {
        private readonly List<Product> _products;
        // IEnumerable encapsulates _products and limits the operations, for instance Enumeration
        // Of course the encapsulation can be overcome by methods like casting and reflection
        public IEnumerable<Product> Products { get { return _products; } }

        public Inventory()
        {
            this._products = new List<Product>();
        }
    }
    public static void Main(string[] args)
    {
        // Aggregate function - Lets say we have an array
        // 1, 2, 3, 4, 5, 6, 7 and we need to find an aggregate (e.g. average, sum, min, max, occassionally count, etc)
        //                                 -> x
        // An Aggregate Function folds (performs aggregration on one element at a time)
        // 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> x

        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // Aggregate Function - Traditional
        var sum = 0; // Seed
        foreach (int item in list)
            sum += item; // Folding

        System.Console.WriteLine($"Sum = {sum}");

        // Aggregate Function - Modern
        var sum2 = list.Aggregate(0 /* Seed */,
                    (accumulator, item) =>
                    {
                        return accumulator + item; /* Folding */
                    });
        System.Console.WriteLine($"Sum2 = {sum2}");

        // MyAggregate Function - Sum
        var sum3 = list.MyAggregate(0, (a, i) => a + i);
        System.Console.WriteLine($"Sum3 = {sum3}");

        // MyAggregate Function - Max
        var max = list.MyAggregate(0, (a, i) => i > a ? i : a);
        System.Console.WriteLine($"Max = {max}");

        // MyConcat Function
        var list1 = new List<int> { 0, 1, 2, 3, 4 };
        var list2 = new List<int> { 5, 6, 7, 8, 9 };
        var combined = list1.MyConcat(list2);
        System.Console.WriteLine($"Concat result:");
        foreach (var item in combined)
            System.Console.WriteLine(item);

        // MyUnion Function
        var list3 = new List<int> { 1, 5, 2, 6, 7 };
        var union = list1.MyUnion(list3);
        System.Console.WriteLine($"Union result:");
        foreach (var item in union)
            System.Console.WriteLine(item);

        // MyUnionWithEqualityComparer Function
        var listString1 = new List<string> {"Hello", "World"};
        var listString2 = new List<string> {"HELLO", "whoa", "STUFF", "stuff"};
        var unionString = listString1.MyUnionWithEqualityComparer(listString2, StringComparer.CurrentCultureIgnoreCase);
        System.Console.WriteLine($"Union with EqualityComparer result:");
        foreach (var item in unionString)
            System.Console.WriteLine(item);

        // Also see MyIntersect, MyExcept and MyToDictionary in LinqWithExtensionMethods



    }

}