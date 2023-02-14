namespace Game;
using Game.Abstract;

public class Engine
{
    private readonly Stack<IEngineState> _states;
    public bool IsRunning { get; private set; }

    public Engine()
    {
        _states = new Stack<IEngineState>();
        IsRunning = true;
    }

    public void Quit()
    {
        IsRunning = false;
    }
    public void PushState(IEngineState state)
    {
        // First check if there are any states in the stack
        if (_states.Count > 0)
            _states.Peek().Deactivate();

        _states.Push(state);
        state.Activate();

    }
    public void PopState(IEngineState state)
    {
        if (_states.Count == 0 || state != _states.Peek())
            throw new InvalidOperationException();

        _states.Pop();
        state.Deactivate();
        state.Dispose();

        if (_states.Count > 0)
            _states.Peek().Activate();

    }

    public void SwitchState(IEngineState state)
    {
        if (_states.Count > 0)
        {
            var oldState = _states.Pop();
            oldState.Deactivate();
            oldState.Dispose();
        }

        _states.Push(state);
        state.Activate();

    }
    public void ProcessInput(ConsoleKeyInfo key)
    {
        if (_states.Count > 0)
            _states.Peek().ProcessInput(key);
    }
}