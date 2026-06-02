using App;
using Game.Commands;
using Game.Interfaces;

namespace Game.IoC;

public class RegisterIoCDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Commands.CommandInjectable",
            (object[] args) => new CommandInjectableCommand()
        ).Execute();
    }
}
