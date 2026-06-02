using App;
using Game.Commands;
using Game.Interfaces;

namespace Game.IoC;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Send",
            (object[] args) =>
            {
                var command = (ICommand)args[0];
                var receiver = (ICommandReceiver)args[1];
                return new SendCommand(command, receiver);
            }
        ).Execute();
    }
}