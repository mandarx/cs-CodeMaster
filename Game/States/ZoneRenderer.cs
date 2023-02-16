using Game.Abstract;
using Game.Model;
using Game.Model.Components;

namespace Game.States;
public class ZoneRenderer : IZoneListener
{

    private readonly Zone _zone;
    private readonly SpriteComponent[,,] _spriteBuffer;
    public bool IsActive { get; set; }

    public ZoneRenderer(Zone zone)
    {
        IsActive = true;
        _zone = zone;
        _spriteBuffer = new SpriteComponent[_zone.Size.X, _zone.Size.Y, _zone.Size.Z];
        foreach (var entity in _zone.Entities)
        {
            var component = entity.GetComponent<SpriteComponent>();
            if (component == null)
                continue;
            _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = component;
        }
    }
    public void RenderAll()
    {
        Console.Clear();
        Console.WriteLine($"ZONE: {_zone.Name.ToUpper()}");
        foreach (var sprite in _spriteBuffer)
        {
            if (sprite == null)
                continue;

            WriteCharacter(sprite.Parent.Position, sprite.Sprite);
            /*
            Console.SetCursorPosition(sprite.Parent.Position.X, sprite.Parent.Position.Y);
            Console.Write(sprite.Sprite);
            */
        }

    }
    public void EntityAdded(Entity entity)
    {
        var sprite = entity.GetComponent<SpriteComponent>();
        if (sprite == null)
            return;

        _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = sprite;
        if (!IsActive)
            return;
        var topMostEntity = GetTopMostEntity(entity.Position);

        if (topMostEntity != null && topMostEntity.Parent.Position.Z > entity.Position.Z)
        {
            return;
        }
        WriteCharacter(entity.Position, sprite.Sprite);
    }

    public void EntityMoved(Entity entity, Vector3 newPosition)
    {
        var sprite = entity.GetComponent<SpriteComponent>();
        if (sprite == null)
            return;

        _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;
        var lastTopMostEntity = GetTopMostEntity(entity.Position);
        var nextTopMostEntity = GetTopMostEntity(newPosition);
        _spriteBuffer[newPosition.X, newPosition.Y, newPosition.Z] = sprite;
        if (!IsActive)
            return;
        



        Console.SetCursorPosition(entity.Position.X, entity.Position.Y);
        if (lastTopMostEntity != null)
            WriteCharacter(entity.Position, lastTopMostEntity.Sprite);
        else
            WriteCharacter(entity.Position, ' ');

        if (nextTopMostEntity == null || nextTopMostEntity.Parent.Position.Z < newPosition.Z)
        {
            WriteCharacter(newPosition, sprite.Sprite);
        }


        _spriteBuffer[newPosition.X, newPosition.Y, newPosition.Z] = sprite;
    }

    public void EntityRemoved(Entity entity)
    {
        var sprite = entity.GetComponent<SpriteComponent>();
        if (sprite == null)
            return;


        _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;

        if (!IsActive)
            return;

        var topMostEntity = GetTopMostEntity(entity.Position);
        if(topMostEntity == null) {
            WriteCharacter(entity.Position, ' ');
            return;
        }
        

        WriteCharacter(topMostEntity.Parent.Position, topMostEntity.Sprite);
    }

    private SpriteComponent GetTopMostEntity(Vector3 position)
    {


        for (var i = _zone.Size.Z - 1; i >= 0; i--)
        {
            var nextEntity = _spriteBuffer[position.X, position.Y, i];
            if (nextEntity == null)
                continue;

            return nextEntity;
        }

        return null;
    }

    private void WriteCharacter (Vector3 position, char character) {
        SetCursorPosition(position);
        Console.Write(character);
    }

    private void SetCursorPosition(Vector3 position) {
        Console.SetCursorPosition(position.X, position.Y+1);
    }
}