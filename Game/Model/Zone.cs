using Game.Abstract;
using Game.Extensions;

namespace Game.Model;

public class Zone
{

    private readonly Entity[,,] _entities;
    public string Name { get; private set; }
    public Vector3 Size { get; private set; }
    private readonly HashSet<IZoneListener> _listeners;
    public IEnumerable<Entity> Entities
    {
        get
        {
            for (var x = 0; x < Size.X; x++)
                for (var y = 0; y < Size.Y; y++)
                    for (var z = 0; z < Size.Z; z++)
                    {
                        var entity = _entities[x, y, z];
                        if (entity == null)
                            continue;
                        yield return entity;
                    }

        }
    }
    public Zone(string name, Vector3 size)
    {
        _listeners = new HashSet<IZoneListener>();
        Size = size;
        Name = name;
        _entities = new Entity[size.X, size.Y, size.Z];
    }

    public void MoveEntity(Entity entity, Vector3 newPosition)
    {
        if (newPosition.X < 0 || newPosition.X >= Size.X || 
            newPosition.Y < 0 || newPosition.Y >= Size.Y ||
            newPosition.Z < 0 || newPosition.Z >= Size.Z
        )
            return;

        var topMostEntity = GetTopMostEntity(newPosition); 
        if (topMostEntity!= null) {
            var component = topMostEntity.GetComponent<IEntityEntranceComponent>();
            if (component != null) {
                if (!component.CanEnter(entity))
                    return;
                component.Enter(entity);
            }
        }


        _listeners.ForEach(l => l.EntityMoved(entity, newPosition));
        _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;
        entity.Position = newPosition;
        _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = entity;
    }

    public void AddEntity(Entity entity)
    {
        _listeners.ForEach(l => l.EntityAdded(entity));
        _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = entity;
    }

    public void RemoveEntity(Entity entity)
    {
        _listeners.ForEach(l => l.EntityRemoved(entity));
        var oldEntity = _entities[entity.Position.X, entity.Position.Y, entity.Position.Z];

        if (oldEntity != entity)
            throw new InvalidOperationException("Entity position is out of sync");

        _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;
    }

    public void AddListener(IZoneListener listener)
    {
        if (!_listeners.Add(listener))
            throw new ArgumentException();
    }

    public void RemoveListener(IZoneListener listener)
    {
        if (_listeners.Remove(listener))
            throw new ArgumentException();
    }

    private Entity GetTopMostEntity (Vector3 position) {
        for (var i=Size.Z -1; i>=0; i--) {
            var entity = _entities[position.X, position.Y, i];
            if (entity!= null)
                return entity;
        }
        return null;
    }
}