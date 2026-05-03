using Game.Interfaces;
using App;

namespace Game.Commands;

public class RotateCommand(IRotatable rotatingObject) : App.ICommand
{
    public void Execute()
    {
        rotatingObject.Orientation += rotatingObject.AngularSpeed;
    }
}
