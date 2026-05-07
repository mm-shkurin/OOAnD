using Game.Interfaces;

namespace Game.Commands;

public class RotateCommand(IRotatable rotatingObject) : ICommand
{
    public void Execute()
    {
        rotatingObject.Orientation += rotatingObject.AngularSpeed;
    }
}
