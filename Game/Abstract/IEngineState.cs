namespace Game.Abstract;

public interface IEngineState : IDisposable
{
    // Handle keystrokes
    void ProcessInput(ConsoleKeyInfo key);
    // Activate state
    void Activate();
    // Deactivate State
    void Deactivate();

}