using Game.Model;

namespace Game.Abstract;

public interface IEntityEntranceComponent: IComponent {

    bool CanEnter(Entity entity);
    void Enter(Entity entity);
}