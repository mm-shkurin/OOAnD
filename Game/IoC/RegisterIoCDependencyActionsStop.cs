using App;
using Game.Commands;
using Game.Interfaces;

namespace Game.IoC;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>("IoC.Register", "Actions.StopCommand",
            (object[] args) => new StopCommand((ICommandInjectable) args[0])).Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Actions.Stop", new Func<object[], object>((args) =>
        {
            var order = (IDictionary<string, object>)args[0];

            var injectable = (ICommandInjectable)order["Injectable"];

            var stopCommand = Ioc.Resolve<ICommand>("Actions.StopCommand", injectable);

            return stopCommand;
        })).Execute();
    }
}