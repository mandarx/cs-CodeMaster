namespace LinqWithExtensionMethods;
using System.Collections;

/**
 *   Requirements for Extension Methods
 *  1. public static method
 *  2. static class
 *  3. should have "this" modifier on IEnumerable<T>
 *
 *  If the Extension Methods are in a different namespace, we can import them with "using"
**/
static class MyLinq
{

    public static T MyFirst<T>(this IEnumerable<T> that)
    {
        foreach (var item in that)
        {
            return item;
        }

        throw new InvalidOperationException();
    }
    public static T MyFirst<T>(this IEnumerable<T> that, Func<T, bool> predicate)
    {
        foreach (var item in that)
            if (predicate(item))
                return item;

        throw new InvalidOperationException();
    }
    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> that, Func<TSource, TResult> projection)
    {
        if (that == null && projection == null)
            throw new ArgumentNullException();

        return MyInnerSelect(that, projection);
    }

    private static IEnumerable<TResult> MyInnerSelect<TSource, TResult>(IEnumerable<TSource> that, Func<TSource, TResult> projection)
    {
        foreach (var item in that)
            yield return projection(item);
    }
    public static bool MyAny<T>(this IEnumerable<T> that)
    {
        foreach (var item in that)
            return true;

        return false;
    }
    public static bool MyAny<T>(this IEnumerable<T> that, Func<T, bool> predicate)
    {
        foreach (var item in that)
            if (predicate(item))
                return true;

        return false;
    }
    public static bool MyAll<T>(this IEnumerable<T> that, Func<T, bool> predicate)
    {
        foreach (var item in that)
            if (!predicate(item))
                return false;

        return true;
    }
    public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> that, Func<T, bool> predicate)
    {
        foreach (var item in that)
            if (predicate(item))
                yield return item;
    }

    // Drawbacks of performing count on IEnumerable include having to enumerate the elements.
    // This means that irrespective of the type of operation (e.g. db lookup, REST API paging, etc), the Count operation has to fetch the items.
    // Another loophole, is with infinite enumerables, e.g. Fibonacci series
    // Note that C# compiler creates an Unspeakable type Just In Time so that we cannot accidentally refer to it.
    public static int MyCount<T>(this IEnumerable<T> that)
    {
        var count = 0;
        foreach (var item in that)
            count++;

        return count;
    }

    public static int MyCount<T>(this IEnumerable<T> that, Func<T, bool> predicate)
    {
        var count = 0;
        foreach (var item in that)
            if (predicate(item))
                count++;

        return count;
    }
    // Following is an optimized MyCount method which use .net features
    // It is not an Iterator Block methods
    // Will not work if the IEnumerable does not also implement ICollection
    public static int MyCountOptimized<T>(this IEnumerable<T> that)
    {
        var collection = that as ICollection<T>;
        if (collection != null)
            return collection.Count;

        throw new InvalidOperationException();
    }

    // Note: Aggregate functions are expected to perform input validation and check boundary conditions, unlike below.
    // Aggregates are basically an abstraction of iterating over a collection and returning a single value.
    public static TAccumulator MyAggregate<TAccumulator, TSource>(this IEnumerable<TSource> that, TAccumulator seed, Func<TAccumulator, TSource, TAccumulator> fold)
    {
        var value = seed;
        foreach (var item in that)
            value = fold(value, item);

        return value;
    }

    // This is a Functional implementation of concatenation.
    // We are not mutating either of the parameters. 
    // We are extracting the operation of concatenating two Enumerables and then yielding them back to the caller.
    // Remember, if MyConcat does not perform deferred concatenation, it cannot be called as Aggregate Function.
    // The specification mentions that Aggregate Function needs to have deferred execution.
    // The advantage of deferred execution is that it yields back and allows the caller to time out, especially in case of infinite enumerables.
    public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
    {
        foreach (var item in that)
            yield return item;
        foreach (var item in rhs)
            yield return item;
    }

    // Remember, typically Unions require only two Enumerables.
    // Chaining multiple Union operations usning the below implementation is not advisable.
    // Chaining requires a more optimised code, which is less readable and requires more maintenance. 
    public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
    {
        return MyUnionWithEqualityComparer(that, rhs, EqualityComparer<T>.Default);
    }

    public static IEnumerable<T> MyUnionWithEqualityComparer<T>(this IEnumerable<T> that, IEnumerable<T> rhs, IEqualityComparer<T> comparer)
    {
        var set = new HashSet<T>(comparer);
        foreach (var item in that)
            if (set.Add(item))
                yield return item;
        foreach (var item in rhs)
            if (set.Add(item))
                yield return item;
    }

    public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
    {
        var exclusionSet = new HashSet<T>(rhs);
        foreach (var item in that)
            if (exclusionSet.Add(item))
                yield return item;
    }

    public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
    {
        var inclusionSet = new HashSet<T>(rhs);
        foreach (var item in that)
            if (inclusionSet.Remove(item))
                yield return item;
    }

    // Look at Linq ToDictionary methods.
    // Note: Parameter checking is usually not done during deferred execution.
    public static IDictionary<TKey, TValue> MyToDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> that, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
    {
        var dictionary = new Dictionary<TKey, TValue>();
        foreach (var item in that)
            dictionary.Add(keySelector(item), valueSelector(item));

        return dictionary;
    }

    [Obsolete("Since we have a non-Generic interface IEnumerable, we can skip TSource", true)]
    public static IEnumerable<TResult> MyCast<TSource, TResult>(this IEnumerable<TSource> that)
    {
        return null;
    }

    public static IEnumerable<TResult> MyCast<TResult>(this IEnumerable that)
    {
        foreach (var item in that)
            yield return (TResult)item;
    }

    public static IEnumerable<TResult> MyOfType<TResult>(this IEnumerable that)
    {
        foreach (var item in that)
            if (item is TResult)
                yield return (TResult)item;
    }

    public static IEnumerable<TResult> MyJoin<TResult, TOuter, TInner, TKey>(
        this IEnumerable<TOuter> that,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> projection,
        IEqualityComparer<TKey> comparer
    )
    {
        foreach (var outerElement in that)
        {
            var outerKey = outerKeySelector(outerElement);
            foreach (var innerElement in inner)
            {
                var innerkey = innerKeySelector(innerElement);
                if (comparer.Equals(outerKey, innerkey))
                    yield return projection(outerElement, innerElement);
            }
        }
    }

    [Obsolete("Method not fully tested", true)]
    public static bool MySequenceEqual<TSource>(this IEnumerable<TSource> that, IEnumerable<TSource> second)
        where TSource : class
    {
        return MySequenceEqual(that, second, EqualityComparer<TSource>.Default);
    }
    [Obsolete("Method not fully tested", true)]
    public static bool MySequenceEqual<TSource>(this IEnumerable<TSource> that, IEnumerable<TSource> second, EqualityComparer<TSource> comparer)
        where TSource : class
    {

        // We could have used nested foreach. But the "using" will allow us to iterate over both sequences together.
        using (var thatEnumerator = that.GetEnumerator())
        {
            using (var secondEnumerator = second.GetEnumerator())
            {
                while (true)
                {
                    var thatHasMoreItems = thatEnumerator.MoveNext();
                    var secondHasMoreItems = secondEnumerator.MoveNext();
                    if (!thatHasMoreItems && !secondHasMoreItems)
                        return true;

                    // When "that" and "second" have different number of elements, it is a discrepancy. 
                    // Two sequences cannot be equal, when they have different number of elements.
                    if (thatHasMoreItems != secondHasMoreItems)
                        return false;

                    // When n'th element of "that" does not match with n'th element of "second", the sequences do not match.
                    // Why not just use if (!thatEnumerator.Current != secondEnumerator.Current) ?
                    // It is because since in this case it is comparison of references and not the actual values within them, we cannot use reference comparison.
                    // We are looking for equality of the values in the objects.
                    if (!comparer.Equals(thatEnumerator.Current, secondEnumerator.Current))
                        return false;

                }
            }
        }
    }

    public static IEnumerable<TResult> MyZip<TSource, TSecond, TResult>(this IEnumerable<TSource> that, IEnumerable<TSecond> second, Func<TSource, TSecond, TResult> projection)
    {
        using(var thatEnumerator = that.GetEnumerator())
        using(var secondEnumerator = second.GetEnumerator())
            while(thatEnumerator.MoveNext() && secondEnumerator.MoveNext())
                yield return projection(thatEnumerator.Current, secondEnumerator.Current);
    }

}

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
        foreach (var item in filtered)
            System.Console.WriteLine(item);
        // Adding a number to numbers
        numbers.Add(10);
        System.Console.WriteLine("IEnumerable, after source List was updated:");
        // Appended number 10 is included while accessing filtered, due to deferred execution
        foreach (var item in filtered)
            System.Console.WriteLine(item);
        // Notice how when IEnumerable is converted to a List, the results are finalised
        System.Console.WriteLine("Filtered List, after source List was updated:");
        foreach (var item in filteredList)
            System.Console.WriteLine(item);

        // Extension Methods created in MyLinq are also available
        var squaredAbove50 = squared.MyWhere(i => i > 50);
        System.Console.WriteLine($"First squared above 50: {squaredAbove50.MyFirst()}");

    }
}