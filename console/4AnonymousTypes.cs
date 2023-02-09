namespace AnonymousTypes;

class Entry
{
    public static void Main(string[] args)
    {

        var peopleDb = new[] {
                new Person(1, "Rainbow", "Dash"),
                new Person(2, "Rarity", "?"),
                new Person(3, "red", "Eye"),
                new Person(4, "Calamity", "Dashite"),
                new Person(5, "Coalgate", "?"),
                new Person(6, "Nelson", "LaQuet")
            };
        // Following is a basic example of Anonymous Type
        // C# compiler generates a class for the following code
        // It will generate an unspeakable type. Debug the following lines to see the runtime types.
        // 1. The properties will always be implied / implicit type. We cannot explicit specify the types.
        // 2. All the properties are read-only
        // 3. We may in exception scenario use reflection to modify the properties, however, it is not advisable.
        var whoa = new
        {
            Prop1 = 10,
            Prop2 = "hey"
        };
        System.Console.WriteLine(whoa.Prop1);

        // Property names in Anonymous Types can also be implied. Look at FirstName property below.
        var person = new Person(32, "ASDFASF", "QWERQWER");
        var whoa1 = new {
            Prop1 = 10,
            Prop2 = "hey",
            person.FirstName
        };
        System.Console.WriteLine(whoa1);
        /* Output
            { Prop1 = 10, Prop2 = hey, FirstName = ASDFASF }
        */

        // The following code will not compile.
        // It is because lambdas do not have a type. They can only be used in the context of a delegate. In this case, the lambda is being used as a Method.
        /*
        var whoa2 = new {
            Stuff = () => {System.Console.WriteLine("hey");};
        };
        whoa2.Stuff();
        */
        // If we want to use lambdas, we need to convert it into a specific delegatte type, e.g. Action:
        var whoa2 = new {
            Stuff = (Action)(() => System.Console.WriteLine("hey"))
        };
        whoa2.Stuff();

        // Anonymous Types were explicitly build for Linq
        // They work very well with a variety of things related to Linq
        // Following is an example
        var grouped = 
            peopleDb.GroupBy(k => k.FirstName[0], (firstNameStartsWith, people) => new {
                firstNameStartsWith,
                Count = people.Count(),
                AverageNameLength = people.Select(p => p.FirstName.Length).Average()
            });
        foreach(var item in grouped)
            System.Console.WriteLine(item);
        /* Output
            { firstNameStartsWith = R, Count = 2, AverageNameLength = 6.5 }
            { firstNameStartsWith = r, Count = 1, AverageNameLength = 3 }
            { firstNameStartsWith = C, Count = 2, AverageNameLength = 8 }
            { firstNameStartsWith = N, Count = 1, AverageNameLength = 6 }
        */

        // We can also query on Anonymous Types
        foreach(var item in grouped.Where(g => g.AverageNameLength > 7))
            System.Console.WriteLine(item);
        /* Output
            { firstNameStartsWith = C, Count = 2, AverageNameLength = 8 }
        */

        // TIP: Anonymous Types are especially useful while querying databases.
        // We can specify the columns we require and the corresponding query will only include the relevant columns.


        // TODO - review GroupBy, GroupJoin, ToLookup, ordering methods and query expressions

    }

    // Generic type inference
    // The following is the only way to accept and return an anonymous type
    static T Identity<T>(T value) {
        return value;
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





}