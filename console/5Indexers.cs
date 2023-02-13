namespace Indexers;



class Entry
{
    public static void Main(string[] args)
    {
        var inventory = new Inventory();
        var product = inventory[20, "whoa"];
        inventory[10, "hey"] = new Product();


    }



}

class Product
{

}
class Inventory
{
    // An indexer looks like this
    public Product this[int id, string blegh]
    {
        get
        {
            System.Console.WriteLine($"Getting {id}");
            return null;
        }
        set { System.Console.WriteLine($"Setting {id} to {blegh}"); }
    }

    // Indexers can also be overridden and overloaded
    public Product this[double test] {
        get{return null;}
        set{}
    }
}

// Indexers can also be specified in interfaces
interface IInventory
{
    Product this[int id, string blegh] { get; set; }
}