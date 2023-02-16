namespace Game.States;

using System;
using Game.Abstract;

public class MainMenuState : IEngineState
{
    public void Activate()
    {
        Console.Clear();
        Console.WriteLine("MAIN MENU!");
    }

    public void Deactivate()
    {
        
    }

    public void Dispose()
    {
        
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        if(key.Key == ConsoleKey.Escape)
            Program.Engine.PopState(this);
    }
}