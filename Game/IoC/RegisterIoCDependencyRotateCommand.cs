using App;
using Game.Commands;
using Game.Interfaces;

namespace Game.IoC;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) =>
            {
                var gameObject = args[0];

                var adapter = Ioc.Resolve<IRotatable>("Adapters.IRotatable", gameObject);

                return new RotateCommand(adapter);
            }
        ).Execute();
    }
}
