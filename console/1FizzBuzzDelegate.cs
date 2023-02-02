// See https://aka.ms/new-console-template for more information

namespace FizzBuzzDelegate;

delegate void FizzBuzzOutput(string output);

class FizzBuzz {

    public static void WriteFizzBuzz(string output) {
        System.Console.WriteLine(output);
    }
    static void Main(string[] args) {
        Run(WriteFizzBuzz, 1, 100);
    }

    public static void Run(FizzBuzzOutput output, int from, int count) {
        for (var i = from; i < from + count; i++) {
            var div3 = i % 3 == 0;
            var div5 = i % 5 == 0;

            if (div3 && div5) {
                output("FizzBuzz");
            } 
            else if (div3) {
                output("Fizz");
            }
            else if (div5) {
                output("Buzz");
            }
            else {
                output(i.ToString());
            }
        }
    }
}

class Entry {
    public static void Main(string[] args) {
        FizzBuzz.Run(FizzBuzz.WriteFizzBuzz, 1, 100);
    }
}





