using System;
using Game.Model;
using Game.Model.Components;
using Game.States;

namespace Game;

public static class Program {
    public static Engine Engine {get; private set;}

    static void Main() {

        const int zoneWidth = 50;
        const int zoneHeight = 30;

        // Following is not supported in Terminal
        //Console.WindowWidth = Console.BufferWidth = zoneWidth;
        //Console.WindowHeight = Console.BufferHeight = zoneHeight + 1;

        Console.CursorVisible = false;


        var player = new Entity();
        player.AddComponent(new SpriteComponent {Sprite = '$'});
        player.Position = new Vector3(2,2,1);

        var tallGrass = new Entity();
        tallGrass.AddComponent(new SpriteComponent{Sprite='#'});
        tallGrass.Position = new Vector3(3,3,0);

        var ceiling = new Entity();
        ceiling.AddComponent(new SpriteComponent{Sprite='@'});
        ceiling.Position = new Vector3(4,4,2);


        var zone1 = new Zone("Zone 1", new Vector3(zoneWidth,zoneHeight,3));
        zone1.AddEntity(player);
        zone1.AddEntity(tallGrass);
        zone1.AddEntity(ceiling);

        Engine = new Engine();
        System.Console.WriteLine("Started");
        Engine.PushState(new ZoneState(player, zone1));

        while (Engine.IsRunning)
            Engine.ProcessInput(Console.ReadKey(true));
    }
}