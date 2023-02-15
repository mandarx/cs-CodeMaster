using Game.Abstract;

namespace Game.Model.Components;

public class ConstantEntranceComponent : IEntityEntranceComponent
{
    private readonly bool _canEnter;
    public ConstantEntranceComponent(bool canEnter) {
        _canEnter = canEnter;
    }

    public Entity Parent { get; set; }

    public bool CanEnter(Entity entity)
    {
        // TODO - check whether the entity can enter
        return _canEnter;
    }

    public void Enter(Entity entity)
    {
        //no-op
    }
}