using Game.Abstract;
using Game.States;

namespace Game.Model.Components;

public class DialogComponent : IEntityEntranceComponent
{
    private readonly IDialog _dialog;
    public DialogComponent (IDialog dialog) {
       _dialog = dialog; 
    }
    public Entity Parent { get; set; }

    public bool CanEnter(Entity entity)
    {
        return true;
    }

    public void Enter(Entity entity)
    {
        Program.Engine.PushState(new DialogState(entity, _dialog));
    }
}