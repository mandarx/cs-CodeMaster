namespace GenericsTypes;

class Entry {
    public static void Main(string[] args) {

    var myInt = 10;
    // var ll = new LinkedList<typeof(myInt)>(); ERROR - need to pass type at compile time. typeof is for runtime.
    // var ll = new LinkedList<myInt.GetType()>();ERROR - same reason as above.
    // The following code compiles but fails at runtime, generic of int cannot be made, because it violates the constraint of type T. Remove the constraing and code will work.
    // var ll = Activator.CreateInstance(typeof(LinkedList<>).MakeGenericType(myInt.GetType())); 
    }

    
}

interface IMyInterface {
    void Method();
}

// where T: class - we may restrict to reference types, value types and struct
// extend one class and implement many interface
// enum cannot be used. Struct can be used instead.
class LinkedList<T> /* : IEnumerable<T> */ where T: class, IMyInterface 
{
    class Node {
        // T value = null; will return compiler exception because T will not be compatible with value types.
        T Value;
        Node Next;
    }

    private Node _first;

    public int Count {get; private set;}

    public void Add<T> (T item) {
        var node = new Node();
        // IMyInterface implementation methods can be accessed as follows
        // node.Value.Method();
    }

    public void Remove<T> (T item) {

    }

    public T Get(int index) {
        // return null; will be a compile time error because it will not be compatible with value types.
        // Therefore we have default operator
        return default(T);
    }

}