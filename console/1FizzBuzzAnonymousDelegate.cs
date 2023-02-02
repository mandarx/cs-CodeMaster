// See https://aka.ms/new-console-template for more information

namespace FizzBuzzAnonymousDelegate;

delegate bool IntPredicate(int number);

class Entry {

    static bool IsMod3 (int number) {
        return number % 3 == 0;
    }

    static bool IsMod5 (int number) {
        return number % 5 == 0;
    }
    public static void Main(string[] args) {

        var arr = new [] {1,2,3,4,5,6};

        var filteredList = Filter(arr, delegate (int number) {
            return number % 3 == 0;
        });

        foreach(var item in filteredList) {
            System.Console.WriteLine(item);
        }
    }

    static IEnumerable<int> Filter (IEnumerable<int> source, IntPredicate predicate) {
        var list = new List<int>();

        foreach(var item in source) {
            if (predicate(item)) 
                list.Add(item);
        }

        return list;
    }

}





