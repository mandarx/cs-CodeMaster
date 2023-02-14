using Game.Model;

namespace Game.Abstract;

public interface IComponent {
    Entity Parent {get; set;}
}