using Game.Model;

namespace Game.Abstract;

public abstract class Component : IComponent
{
    public Entity Parent { get; set; }
}