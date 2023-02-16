using Game.Model;

namespace Game.Abstract;

public interface IDialogScreen {
    bool FinalScreen {get;}
    string Text{get;}
    Dictionary<string, IDialogScreen> NextScreens {get;}

    void EnterScreen(Entity entity);
}