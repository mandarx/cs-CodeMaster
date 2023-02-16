using Game.Abstract;

namespace Game.Model;

public class Player {
    private readonly List<IItem> _equippedItems;
    private readonly List<IItem> _inventory;
    public IEnumerable<IItem> EquipedItems{get {return _equippedItems;}}
    public IEnumerable<IItem> Inventory {get {return _inventory;}}

    public Player() {
        _equippedItems = new List<IItem>();
        _inventory = new List<IItem>();
    }

    public void AddItem (IItem item) {
        _inventory.Add(item);
    }
    public void EquipItem (IItem item) {
        _inventory.Remove(item);
        _equippedItems.Add(item);
    }

    public void UnequipItem (IItem item) {
        _inventory.Add(item);
        _equippedItems.Remove(item);
    }
}