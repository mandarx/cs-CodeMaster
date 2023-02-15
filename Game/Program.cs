using System;
using Game.Model;
using Game.Model.Components;
using Game.States;

namespace Game;

public static class Program
{
    public static Engine Engine { get; private set; }

    static void Main()
    {

        const int zoneWidth = 50;
        const int zoneHeight = 30;

        // Following is not supported in Terminal
        //Console.WindowWidth = Console.BufferWidth = zoneWidth;
        //Console.WindowHeight = Console.BufferHeight = zoneHeight + 1;

        Console.CursorVisible = false;


        var player = new Entity();
        player.AddComponent(new SpriteComponent { Sprite = '$' });
        player.AddComponent(new PlayerComponent(new Player()));
        player.Position = new Vector3(2, 2, 1);

        var tallGrass = new Entity();
        tallGrass.AddComponent(new SpriteComponent { Sprite = '#' });
        tallGrass.Position = new Vector3(3, 3, 0);

        var ceiling = new Entity();
        ceiling.AddComponent(new SpriteComponent { Sprite = '@' });
        ceiling.Position = new Vector3(4, 4, 2);

        var wall = new Entity();
        wall.AddComponent(new ConstantEntranceComponent(false));
        wall.AddComponent(new SpriteComponent { Sprite = 'X' });
        wall.Position = new Vector3(5, 5, 0);

        var npc1 = new Entity();
        npc1.AddComponent(new DialogComponent(new Dialog(
            new DialogScreen("Hey there!", nextScreens: new Dictionary<string, Abstract.IDialogScreen>
            {
                {"Option 1", new DialogScreen("Option 1", (entity) => System.Console.WriteLine("ACTION 1"))},
                {"Option 2", new DialogScreen("Option 2", (entity) => System.Console.WriteLine("ACTION 2"), 
                    new Dictionary<string, Abstract.IDialogScreen>{{"More Stuff", new DialogScreen("FINAL SCREEN")},}
            )},
            })
        )));
        npc1.AddComponent(new SpriteComponent { Sprite = '!' });
        npc1.Position = new Vector3(1, 1, 0);


        var zone1 = new Zone("Zone 1", new Vector3(zoneWidth, zoneHeight, 3));
        zone1.AddEntity(player);
        zone1.AddEntity(tallGrass);
        zone1.AddEntity(ceiling);
        zone1.AddEntity(wall);
        zone1.AddEntity(npc1);

        Engine = new Engine();
        System.Console.WriteLine("Started");
        Engine.PushState(new ZoneState(player, zone1));

        while (Engine.IsRunning)
            Engine.ProcessInput(Console.ReadKey(true));
    }
}