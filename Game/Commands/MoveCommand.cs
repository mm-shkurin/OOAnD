using App;
using Game.Interfaces;
using Game.Models;

namespace Game.Commands;

public class MoveCommand(IMovable movingObject) : ICommand
{
    public void Execute()
    {
        movingObject.Position += movingObject.Speed;
    }
}
