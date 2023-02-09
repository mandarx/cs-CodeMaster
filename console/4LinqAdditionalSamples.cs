namespace LinqAdditionalSamples;
using System.Collections.Generic;
using LinqWithExtensionMethods;

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

        // Below code demostrates grouping with ToLookup
        /*--------------------------------------------*/
        var peopleDb = new[] {
                new Person("Rainbow", "Dash"),
                new Person("Rarity", "?"),
                new Person("Calamity", "Dashite"),
                new Person("Coalgate", "?"),
                new Person("Nelson", "LaQuet")
            };

        ILookup<char, Person> lookup = peopleDb.ToLookup(k => k.FirstName.Length > 0 ? k.FirstName[0] : ' ');
        foreach (var people in lookup)
        {
            System.Console.WriteLine("Key {0}", people.Key);

            foreach (var person in people)
            {
                Console.WriteLine(" -- {0} {1}", person.FirstName, person.LastName);
            }
        }
        /* Output
            Key R
            -- Rainbow Dash
            -- Rarity ?
            Key C
            -- Calamity Dashite
            -- Coalgate ?
            Key N
            -- Nelson LaQuet
        */

        // Perform case insensitive lookups
        peopleDb = new[] {
                new Person("Rainbow", "Dash"),
                new Person("Rarity", "?"),
                new Person("red", "Eye"),
                new Person("Calamity", "Dashite"),
                new Person("Coalgate", "?"),
                new Person("Nelson", "LaQuet")
            };
        var anotherLookup = peopleDb.ToLookup(k => k.FirstName.Length > 0 ? k.FirstName[0].ToString() : " ", StringComparer.CurrentCultureIgnoreCase);
        var firstNameWithR = anotherLookup["r"];
        foreach (var person in firstNameWithR)
            Console.WriteLine("{0}", person.FirstName);

        /* Output
            Rainbow
            Rarity
            red
        */
        /*--------------------------------------------*/

        // Perform joins with MyJoin
        peopleDb = new[] {
                new Person(1, "Rainbow", "Dash"),
                new Person(2, "Rarity", "?"),
                new Person(3, "red", "Eye"),
                new Person(4, "Calamity", "Dashite"),
                new Person(5, "Coalgate", "?"),
                new Person(6, "Nelson", "LaQuet")
            };
        var addressDb = new[] {
                new Address(1, "Cloudsdale"),
                new Address(1, "Stuff"),
                new Address(2, "Carasel Boteque"),
                new Address(4, "Pegasie Enclave")
            };
        var addresses = peopleDb.MyJoin(
            addressDb, o => o.Id, 
            i => i.PersonId, 
            (p, a) => $"{p.FirstName} lives in {a.PostalAddress}", 
            EqualityComparer<int>.Default);

        foreach(var address in addresses)
            System.Console.WriteLine(address);

        /* Output
            Rainbow lives in Cloudsdale
            Rainbow lives in Stuff
            Rarity lives in Carasel Boteque
            Calamity lives in Pegasie Enclave
        */

        // Group By
        // Key criteria available for group by
        // 1. keySelector
        // 2. elementSelector
        // 3. resultSelector
        // For instance get all people whose first name ends with a 'y'
        var group = peopleDb.GroupBy(k => k.FirstName.EndsWith("y", StringComparison.CurrentCultureIgnoreCase));
        foreach(var people in group) {
            System.Console.WriteLine(people.Key);
            foreach(var person in people)
                System.Console.WriteLine($"-- {person.FirstName} {person.LastName}");
        }

        // The key difference berween ToLookup and GroupBy is that GroupBy is deferred
        // 1. GroupBy does not flatten the source enumerable
        // 2. Since GroupBy is deferred, changes made to the source enumerable will result in different results
        // 3. Unlike a ToLookup, which has an Indexer and Count, a GroupBy does not return these


        // The code above can also be implemented using a ResultsSelector, as follows
        IEnumerable<string> results = peopleDb.GroupBy(k => k.FirstName.Length > 0 ? k.FirstName[k.FirstName.Length - 1] : ' ', 
            (k,r) => $"There are {r.Count()} people whose first name ends with '{k}'"
        );
        foreach(var people in results)
            System.Console.WriteLine(people);

        
        // SequenceEqual
        // TODO


        // Zip
        // Zip takes two sequences, enumerates them simultaneously and for every item in both the enumerable, it applies the projection and yields the result
        // If there is a mismatch in the number of elements, the Zip method stops execution when reach the end of either sequence.
        peopleDb = new[] {
                new Person(1, "Rainbow", "Dash"),
                new Person(2, "Rarity", "?"),
                new Person(3, "red", "Eye"),
                new Person(4, "Calamity", "Dashite"),
                new Person(5, "Coalgate", "?"),
                new Person(6, "Nelson", "LaQuet")
            };
        var notes = new [] {"Note 1", "Note 2", "Note 3", "Note 4", "Note 5", "Note 6"};

        var zipped = peopleDb.MyZip(notes, (p, n) => $"Person named {p.FirstName} has these notes {n}");
        foreach(var item in zipped)
            System.Console.WriteLine(item);


        // Implicit typing
        // Anonymous types - An Anonymous Type is a class that has no actual definition

        


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

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    class Address
    {

        public int PersonId { get; set; }
        public string PostalAddress { get; set; }
        public Address(int personId, string address)
        {
            PersonId = personId;
            PostalAddress = address;
        }

    }
}