namespace GenericsBasics;

class Entry {
    public static void Main(string[] args) {
        var stuff = new Stuff<string>();
        stuff.Method("Hi");

        var stuffInt = new Stuff<int>();
        stuffInt.Method(25);

        var type = typeof(Stuff<>);
        var type2 = typeof(Stuff<>).MakeGenericType(typeof(int));

        System.Console.WriteLine(type);
        System.Console.WriteLine(type2);
    }

    
}

class Stuff<Tkey> {
    public void Method(Tkey key) {
        System.Console.WriteLine(key!.ToString());
    }
}