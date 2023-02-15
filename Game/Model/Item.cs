using Game.Abstract;

namespace Game.Model;

public class Item : IItem
{
    public bool CanEquip {get; private set;}

    public bool CanUse {get; private set;}

    public string Name {get; private set;}

    public Item(string name, bool canEquip, bool canUse) {
        Name = name;
        CanEquip = canEquip;
        CanUse = canUse;
    }
}