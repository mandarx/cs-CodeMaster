// See https://aka.ms/new-console-template for more information

namespace FizzBuzzTraditional;


interface IFizzOutput {
    void Write(string output);
}

class ConsoleFizzOutput : IFizzOutput
{
    public void Write(string output)
    {
        System.Console.WriteLine(output);
    }
}

class FizzBuzz {
    private readonly IFizzOutput _output;

    public FizzBuzz(IFizzOutput output) {
        this._output = output;
    }

    public void Run(int from, int count) {
        for (var i = from; i < from + count; i++) {
            var div3 = i % 3 == 0;
            var div5 = i % 5 == 0;

            if (div3 && div5) {
                _output.Write("FizzBuzz");
            } 
            else if (div3) {
                _output.Write("Fizz");
            }
            else if (div5) {
                _output.Write("Buzz");
            }
            else {
                _output.Write(i.ToString());
            }
        }
    }
}

class Entry {
    public static void Main(string[] args) {
        var FizzBuzz = new FizzBuzz(new ConsoleFizzOutput());
        FizzBuzz.Run(1, 100);
        try {
            Console.ReadKey();
        } catch(InvalidOperationException ioe) {
            System.Console.WriteLine("ReadKey failed. Operation Invalid. This is expected.");
        }
        
    }
}





