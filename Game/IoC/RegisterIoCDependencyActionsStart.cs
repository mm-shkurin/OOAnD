using App;
using Game.Interfaces;
using Game.IoC;
using Game.Commands;

namespace Game.IoC;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        new RegisterIoCDependencyMacroCommand().Execute();
        new RegisterIoCDependencySendCommand().Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Actions.StartCommand",
            (object[] args) => new StartCommand((ICommand)args[0], (ICommandReceiver)args[1]))
            .Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Actions.Start", (object[] args) =>
        {
            var order = (IDictionary<string, object>)args[0];
            var target = order["target"];
            var command = (string)order["command"];
            var receiver = (ICommandReceiver)order["receiver"];

            var longOperation = Ioc.Resolve<ICommand>(command, new object[] { target });
            var startCommand = Ioc.Resolve<ICommand>("Actions.StartCommand", longOperation, receiver);

            return startCommand;
        }).Execute();
    }
}