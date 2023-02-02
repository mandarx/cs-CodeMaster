// See https://aka.ms/new-console-template for more information

namespace FizzBuzzLambdaHoisting;

delegate bool IntPredicate(int number);

class Entry {
    public static void Main(string[] args) {

        var arr = new [] {1,2,3,4,5,6};

        int number = 10;
        var filteredList = Filter(arr,  n => {
            number++;
            return n % 3 == 0;
        } );
        System.Console.WriteLine("Number: " + number);
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





