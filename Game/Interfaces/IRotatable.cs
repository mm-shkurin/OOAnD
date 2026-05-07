using Game.Models;

namespace Game.Interfaces;

public interface IRotatable
{
    Angle Orientation { get; set; }
    Angle AngularSpeed { get; }
}
