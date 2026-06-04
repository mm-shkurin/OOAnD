using App;
using App.Scopes;
using Game.Commands;
using Game.Interfaces;
using Game.IoC;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests;

public class RegisterIoCDependencyActionsStopTests
{
    public RegisterIoCDependencyActionsStopTests()
    {
        new InitCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void RegisterActionStop_Injectable_ShouldRegisterAndResolveCorrectly()
    {
        var mockInjectable = new Mock<ICommandInjectable>();
        var order = new Dictionary<string, object>
        {
            { "Injectable", mockInjectable.Object }
        };

        var registerCommand = new RegisterIoCDependencyActionsStop();
        registerCommand.Execute();

        var stopCommand = Ioc.Resolve<ICommand>("Actions.Stop", order);
        stopCommand.Execute();

        mockInjectable.Verify(
            x => x.Inject(It.IsAny<EmptyCommand>()),
            Times.Once);
    }
}
