namespace ImplicitConversions;

class Entry
{

    public static void Main(string[] args)
    {
        // C# performs certain type conversions by default
        // e.g. int to long
        long a = 2;

        // But certain other implicit conversions are not allowed
        // e.g. float to long
        // long a = 2.5F;

        // Let us say we have Vector3 but we have another method which accepts Vector2
        var vector3 = GetInformationFromDatabase();
        // Typically we would do the following
        SizeConsoleWindow(new Vector2(vector3.X, vector3.Y));
        // But this means we need to explicitly specify the conversion.

        // Now refer the implicit conversion methods in Vector2 and Vector3
        // This will now allow us perform actions as follows, although it is not recommended:
        Vector2 vect = new Vector3(1, 2, 3);

        // You should always use implicit typing as follows:
        var vect2 = (Vector2)new Vector3(1, 2, 3);

        // Interesting side note. We get an InvalidCastException below.
        // It is because, Vector3 to object is implicit conversion.
        // But we need and explicit conversion from object to Vector2.
        // vect2 = (Vector2)(object)new Vector3(1,2,3);
        // System.Console.WriteLine(vect2);

        // While an implicit conversion can handle both implicit and explicit conversions,
        // An explicit conversion can handle only explicit conversions.
        // vect2 = new Vector4(1, 2, 3); // compilation error
        // Only explicit conversion works
        vect2 = (Vector2)new Vector4(1, 2, 3);

        // The rule of thumb is, you should only use implicit conversions, where there is no loss of data
        // e.g. While converting from Vector3 / Vector4 to Vector2, the Z property is lost.

        // Also note: User defined conversions do not work at runtime
        // The following code throws error
        // Blegh(new Vector4(10,10,10));
        /* Output
            Unhandled exception. System.InvalidCastException: Unable to cast object of type 'ImplicitConversions.Vector4' to type 'ImplicitConversions.Vector2'.
        */

        // There are compile time convertions and runtime conversions
        // At compile time, the following code is transformed as shown below
        var hey = (Vector2)new Vector4(1, 1, 1);
        //  var hey = Vector4.explicit_operator_Vector2(new Vector4(1, 1, 1));

    }

    static Vector3 GetInformationFromDatabase()
    {
        return new Vector3(3, 2, 1);
    }
    static void SizeConsoleWindow(Vector2 vector2)
    {

    }

    // The following code is bad, because we have created compilable code which is doomed to throw exceptions at runtime
    // It is safer to use Generics, because we will get compile time errors to ensure type safety.
    static void Blegh(object whoa)
    {
        // At compile time, the compiler treats the below code as follows, if there is no explicit conversion:
        // 1. What is the type of whoa?
        // 2. Does the COMPILE TIME TYPE have a conversion?
        // 3. It is an object, so, no.
        // Since whoa is of the type "object", the caller could pass anything and the compiler has no idea whether it could be converted. Hence there is no compile time error.
        // To get an error at compile time, we may use Generics, as the compiler now needs to check conversions for T
        Vector2 vector2 = (Vector2)whoa;
        // At runtime, op-code for the above line looks as follows:
        // if (!(whoa is Vector2))
        //      throw new InvalidCastException();
        //  vector2 = whoa;
        System.Console.WriteLine(vector2.X);
    }

    // An implementation of above method which uses Generics
    // It should be used while doing generic things
    static void Blegh2<T>(T whoa) where T : Vector2
    {
        var vector2 = (Vector2)whoa;
        System.Console.WriteLine(vector2.X);
    }

    // The safest implementation is below
    // Here we are being very clear that we only expect a specific type
    static void Blegh3(Vector2 vector2)
    {
        System.Console.WriteLine(vector2.X);
    }

}

class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }
    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    // Method added later for demonstrating implicit conversions
    // All implicit conversions look as follows
    // public static implicit operator <TargetType>(<SourceType> source)
    // The implicit conversion method can be present in either the source or target class, but not elsewhere
    public static implicit operator Vector3(Vector2 rhs)
    {
        return new Vector3(rhs.X, rhs.Y, 0);
    }

    // For instance the following will give a compilation error
    // Error: User-defined conversion must convert to or from the enclosing type
    /*
    public static implicit operator int(float rhs) {

    }
    */

}

class Vector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Method added later for demonstrating implicit conversions
    [Obsolete("Implicit conversions should be used only when there no loss of data. Here, we will lose the Z property.")]
    public static implicit operator Vector2(Vector3 rhs)
    {
        return new Vector2(rhs.X, rhs.Y);
    }

    // Implicit operators may be abused.
    // Never do something like the following.
    // Ideally this could be in a component external to this class.
    [Obsolete("Do not abuse implicit conversion", true)]
    public static implicit operator Vector3(string str)
    {
        // string parsing code
        return new Vector3(1, 2, 3);
    }
}

class Vector4
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public Vector4(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Method added later for demonstrating explicit conversions
    public static explicit operator Vector2(Vector4 rhs)
    {
        return new Vector2(rhs.X, rhs.Y);
    }
}