

using Game.Abstract;

namespace Game.Model.Components;

public class PlayerComponent : Component
{
    private readonly Player _player;
    public PlayerComponent(Player player)
    {
        _player = player;
    }
}