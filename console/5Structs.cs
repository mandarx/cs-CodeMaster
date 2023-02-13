namespace Structs;


// A struct is a user defined type
// Key points differentiating structs from classes
// 1. No inheritance - struct cannot inherit or be inherited from
// 2. No Default constructor for struct - there is an implicit default constructor
// 3. All constructors must specify a value for each of their fields.

struct Vector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        // Backing fields are unspeakable. The following code will fail unless the this()
        X = x;
        Y = y;
    }

    public void GoForward()
    {
        X += 20;
    }
}

class Whoa
{
    public Vector2 Position { get; set; }
}

class Entry
{
    public static void Main(string[] args)
    {
        var vect2 = new Vector2(1, 2);
        Mutate(vect2);
        System.Console.WriteLine(vect2.X);
        // Output
        /* 
            1
        */
        // The above result is because struct is passed by value.

        var hey = new Whoa();
        hey.Position.GoForward();
        System.Console.WriteLine(hey.Position.X);
        // Output
        /* 
            0
        */
        // When we do hey.Position, we get a copy. Therefore, mutations to the copy are lost.

        vect2 = new Vector2();
        vect2.GoForward();
        System.Console.WriteLine(vect2.X);
        // Output
        /* 
            20
        */
        // structs can be mutated when we have the actual value.

        // Best Practice: You should avoid writing a Mutable struct
        // In other words, avoid mutating value types because they are passed by value.

        // In the below code, vect2 inside the Hey method will always be in the heap, becuase it is inside a lambda
        // Normally, it would be an implementation detail.
        var action = Hey();
        action();

        // Other details
        // 1. All value types can be boxed
        // Argument of Hey1 is of type object
        // Note: Boxing can cause adverse performance issues.
        // e.g. a struct would just occupy a few bytes, to store values, whereas objects will occupy greater memory to store additional object header and pointers
        Hey1(vect2);

        // Value types cannot be null, unless you make them Nullable
        Vector2? whoa = null;

        
        var vector1 = new Vector2(2,1);
        var vector2 = new Vector2(2,3);
        // structs cannot be compared using the == operator
        // Following code creates a compiler error
        /*
        if (vector1 == vector2) {
            //no-op
        }
        */

        // When to use structs
        // 1. Performance optimization - remember, you should not do performance optimiztion, unless you are aware of the problem


    }

    // Will not work. Structs are value types.
    // Like enums or integers or floats or doubles.
    static void Mutate(Vector2 whoa)
    {
        whoa.X = 10;
    }

    static Action Hey()
    {
        Vector2 vect2 = new Vector2(1, 2);
        Action whoa = () =>
        {
            // vect2 is hoisted into the whoa lambda
            System.Console.WriteLine(vect2.X);
        };
        return whoa;
    }


    static void Hey1(object whoa)
    {
        // no-op
    }

    interface IMyInterface
    {
        void Hey();
    }

    // struct can implement interfaces
    struct Vector3 : IMyInterface
    {
        public void Hey()
        {
            //no-op
        }
    }

}