namespace Objects;


interface IBlegh
{

}

// Best practice: Whenever we override the Equals method, we should also implement IEquatable interface
class Vector2 : IBlegh, IEquatable<Vector2>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Problems with this code
    // 1. We have redundancy with == and != methods
    // 2. They do not count for null
    // So we will delegate to the Equals method
    /*
    public static bool operator ==(Vector2 lhs, Vector2 rhs)
    {
        return lhs.X == rhs.X && lhs.Y == rhs.Y;
    }
    public static bool operator !=(Vector2 lhs, Vector2 rhs)
    {
        return lhs.X != rhs.X || lhs.Y != rhs.Y;
    }
    */

    public static bool operator ==(Vector2 lhs, Vector2 rhs)
    {
        return object.Equals(lhs, rhs);
    }
    public static bool operator !=(Vector2 lhs, Vector2 rhs)
    {
        return !object.Equals(lhs, rhs);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (!(obj.GetType() != typeof(Vector2)))
            return false;

        return Equals((Vector2)obj);
    }

    public bool Equals(Vector2? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return X == other.X && Y == other.Y;
    }

    // The GetHashCode, if implemented, should always return the same value, given a certain combination of its properties.
    // Following is a basic implementation
    public override int GetHashCode()
    {
        return (X.GetHashCode() * 397) ^ Y.GetHashCode();
    }
}

class Entry
{
    public static void Main(string[] args)
    {


        var vector1 = new Vector2(1, 1);
        var vector2 = new Vector2(1, 1);

        if (vector1 == vector2)
        {
            System.Console.WriteLine("EQUALS!");
        }
        else
        {
            System.Console.WriteLine("NOT EQUALS!");
        }

        Compare(vector1, vector2);

        // A better implementation is to override equals
        System.Console.WriteLine(vector1.Equals(vector2));

        // Let us consider a Person and an Address
        // A Person usually has an identifier. Therefore, to find the equality of two Persons, we must use the identifier.
        // Whereas for an Address, we must consider using the sum of its elements.



    }

    static void Compare(object obj1, object obj2)
    {
        // Given that both parameters are objects, C# compiler has no way of inferring which == overload to call.
        // Note: All operators are resolved at compile time
        if (obj1 == obj2)
        {
            System.Console.WriteLine("Object EQUALS!");
        }
        else
        {
            System.Console.WriteLine("Object NOT EQUALS!");
        }
    }

    static void InterfaceCompare(IBlegh obj1, IBlegh obj2)
    {
        // Even if we use interface references, we get the same output as the Compare method
        // It is because Interfaces could be implemented by many classes. And the compiler would still resolve to the object reference equality.
        if (obj1 == obj2)
        {
            System.Console.WriteLine("Object EQUALS!");
        }
        else
        {
            System.Console.WriteLine("Object NOT EQUALS!");
        }
    }

}