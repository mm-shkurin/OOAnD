using App;
using Game.Interfaces;
using Game.Models;

namespace Game.IoC;

public class RegisterIoCDependencyAuthorizer : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Game.Authorizer",
            (object[] args) => new PrefixTreeAuthorizer()
        ).Execute();
    }
}