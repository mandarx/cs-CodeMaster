namespace IteratorBlock;

class Entry {
    public static void Main(string[] args) {
        foreach(var item in MyIteratorBlock(5, 10)) {
            System.Console.WriteLine(item);
        }
    }

    static IEnumerable<int> MyIteratorBlock(int from, int to) {
        // The Iterator block gets turned into a state machine.
        // Whenever the execution reaches "yield return", it returns the current item
        // and discontinues the execution, until the calling foreach loop asks for another item
        // yield return and yield break are two valid ways of using yield
        for (var i = from; i <= to ; i++) {
            yield return i * i;
        }
    }
}