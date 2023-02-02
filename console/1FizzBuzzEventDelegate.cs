// See https://aka.ms/new-console-template for more information

namespace FizzBuzzEventDelegate;

delegate void ButtonClick(Button button);

class Button {
    public event ButtonClick Click;

    public void SimulateClick() {
        if (Click != null) {
            Click(this);
        }
    }
}

class Entry {
    public static void Main(string[] args) {

        var button = new Button();
        button.Click += ButtonClickedBehavior;
        button.Click += OtherButtonClickedBehavior;

        button.SimulateClick();
    }

    static void ButtonClickedBehavior(Button button) {
        System.Console.WriteLine("Button Clicked");
    }

    static void OtherButtonClickedBehavior(Button button) {
        System.Console.WriteLine("Other Button Clicked");
    }

}





