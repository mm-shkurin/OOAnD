using App;
using Game.Commands;
using Game.Interfaces;

namespace Game.IoC;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Move",
            (object[] args) =>
            {
                var gameObject = args[0];

                var adapter = Ioc.Resolve<IMovable>("Adapters.IMovable", gameObject);

                return new MoveCommand(adapter);
            }
        ).Execute();
    }
}
