namespace Game.Abstract;

public interface IItem
{
    string Name { get; }
    bool CanEquip { get; }
    bool CanUse { get; }
}