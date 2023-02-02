// See https://aka.ms/new-console-template for more information

namespace FizzBuzzDelegateInvocationList;

delegate void FizzBuzzOutput(string output);

class Blegh {
    private readonly string _prefix;

    public Blegh(string prefix) {
        this._prefix = prefix;
    }

    public void DoStuff(string output) {
        System.Console.WriteLine("From Blegh {0} - {1}", _prefix, output);
    }

    public override string ToString()
    {
        return "Blegh with prefix of " + _prefix;
    }
}

class FizzBuzz {

    public static void WriteFizzBuzz(string output) {
        System.Console.WriteLine(output);
    }
    static void Main(string[] args) {
        Run(WriteFizzBuzz, 1, 100);
    }

    public static void Run(FizzBuzzOutput output, int from, int count) {
        //System.Console.WriteLine("Target = {0}", output.Target.ToString());
        //System.Console.WriteLine("Method = {0}", output.Method.ToString());

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

        var blegh1 = new Blegh("Lil'pip");
        var blegh2 = new Blegh("Calamity");
        var blegh3 = new Blegh("Rainbow Dash");

        FizzBuzzOutput myOutput = blegh1.DoStuff;
        myOutput += blegh2.DoStuff;
        myOutput += blegh3.DoStuff;
        myOutput += FizzBuzz.WriteFizzBuzz;

        FizzBuzz.Run(myOutput, 1,3);

        //FizzBuzz.Run(blegh1.DoStuff, 1,3);
        //FizzBuzz.Run(blegh2.DoStuff, 1,3);

        //FizzBuzz.Run(FizzBuzz.WriteFizzBuzz, 1, 100);
    }
}





