namespace OperatorOverloading;

class Entry
{
    public static void Main(string[] args)
    {
        // Overloadable unary operators:
        /*
            +, -, ~, !, ++, --, true, false
        */

        // Overloadable binary operators:
        /*
            +, -, *, /, ^, <<, >>, &, |
        */

        // Overloadable conditional operators:
        /*
            ==, !=, <, >, >=, <=
        */

        // Operators which cannot be overloaded:
        /*
            =, [], (), ., ternary, pointer, sizeof, etc
        */

        var vect1 = new Vector3(1, 2, 3);
        var vect2 = new Vector3(2, 3, 4);

        // Overridden multiplication operator.
        var whoa = vect1 * 2;
        // By overloading the multiplication operator, we have implicitly overridden the multiplication assignment operator also.
        whoa *= 2;
        // Overloaded ++ operator
        whoa = whoa++;

        // Overloaded unary true/false operator
        if (whoa)
        {
            System.Console.WriteLine("WHOA");
        }
        else
        {
            System.Console.WriteLine("HEY");
        }
        // Note: In the above statement, we cannot check for equivalence
        // e.g. the following will fail
        // if(whoa == false)

        // Note: Avoid the tendency to overload operators unnecessarily
        // e.g. var invoice = customer + order;

        // Usually operator overloading will be required while performing Math operations.
        // Operator overloading is rarely justifiable otherwise.

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

        // Note: Read the operator overloading rules and specifications
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vector3 operator *(Vector3 lhs, int rhs)
        {
            return new Vector3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        // Remember
        // 1. The order of the parameters matter. e.g. (Vector3 lhs, int rhs) != (int lhs, Vector3 rhs)
        // 2. The type of the parameters matter.
        // 3. The type of the return value matters.
        // Therefor consider using operator overloading carefully.
        public static string operator *(int lhs, Vector3 rhs)
        {
            return "hey";
        }

        // Certain operators cannot be overloaded in certain conditions
        // e.g. By overloading the multiplication operator, we have implicitly overloaded the multiplation assignment operator
        /*
        public static Vector3 operator *= (Vector3 lhs) {
            // implementation
        }
        */

        //Following does not work.
        //Atleast one argument should be of the enclosing type.
        /*
        public static Vector3 operator *(int lhs, int rhs)
        {
            return new Vector3(0, 0, 0);
        }
        */

        public static Vector3 operator ++(Vector3 lhs)
        {
            return new Vector3(lhs.X + 1, lhs.Y + 1, lhs.Z + 1);
        }

        // To implement the unary true operator, you must also implement the unary false operator
        public static bool operator true(Vector3 lhs)
        {
            return false;
        }
        public static bool operator false(Vector3 lhs)
        {
            return true;
        }

    }


}