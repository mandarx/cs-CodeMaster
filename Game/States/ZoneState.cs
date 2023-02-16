using Game.Abstract;
using Game.Model;

namespace Game.States;

public class ZoneState : IEngineState
{

    private readonly Entity _player;
    private readonly Zone _zone;
    private readonly ZoneRenderer _renderer;

    public ZoneState(Entity player, Zone zone)
    {
        _zone = zone;
        _player = player;
        _renderer = new ZoneRenderer(zone);

        _zone.AddListener(_renderer);
    }
    public void Activate()
    {
        //Console.Clear();
        //Console.WriteLine("IN ZONE!");
        _renderer.IsActive = true;
        _renderer.RenderAll();
    }

    public void Deactivate()
    {
        _renderer.IsActive = false;
    }

    public void Dispose()
    {
        _zone.RemoveListener(_renderer);

    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        var position = _player.Position;
        if (key.Key == ConsoleKey.Escape)
            Program.Engine.PushState(new MainMenuState());
        else if (key.Key == ConsoleKey.W)
            _zone.MoveEntity(_player, new Vector3(position.X, position.Y - 1, position.Z));
        else if (key.Key == ConsoleKey.A)
            _zone.MoveEntity(_player, new Vector3(position.X - 1, position.Y, position.Z));
        else if (key.Key == ConsoleKey.S)
            _zone.MoveEntity(_player, new Vector3(position.X, position.Y + 1, position.Z));
        else if (key.Key == ConsoleKey.D)
            _zone.MoveEntity(_player, new Vector3(position.X + 1, position.Y, position.Z));
    }
}