using Game.Models;

namespace Game.Interfaces;

public interface IMovable
{
    Vector Position { get; set; }
    Vector Speed { get; }
}
