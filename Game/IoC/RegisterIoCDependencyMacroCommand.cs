using App;
using Game.Commands;
using Game.Interfaces;

namespace Game.IoC;

public class RegisterIoCDependencyMacroCommand : App.ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Macro",
            (object[] args) => new MacroCommand((ICommand[])args[0])
        ).Execute();
    }
}