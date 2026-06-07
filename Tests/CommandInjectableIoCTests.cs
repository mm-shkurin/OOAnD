using App;
using App.Scopes;
using Game.IoC;
using Game.Commands;

namespace Tests;

public class CommandInjectableIoCTests
{
    public CommandInjectableIoCTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependency_CommandInjectable_ShouldResolveWithoutException()
    {
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();
        registerCommand.Execute();

        var resolvedAsICommand = Ioc.Resolve<ICommand>("Commands.CommandInjectable");
        var resolvedAsInjectable = Ioc.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var resolvedAsConcrete = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");

        Assert.NotNull(resolvedAsICommand);
        Assert.NotNull(resolvedAsInjectable);
        Assert.NotNull(resolvedAsConcrete);
        Assert.IsType<CommandInjectableCommand>(resolvedAsConcrete);
    }
}
