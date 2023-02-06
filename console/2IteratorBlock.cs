namespace IteratorBlock;

class Entry {
    public static void Main(string[] args) {
        foreach(var item in MyIteratorBlock(5, 10)) {
            System.Console.WriteLine(item);
        }
    }

    static IEnumerable<int> MyIteratorBlock(int from, int to) {
        for (var i = from; i <= to ; i++) {
            yield return i * i;
        }
    }
}