using App;
using App.Scopes;
using Moq;
using Game.Commands;
using Game.Interfaces;
using Game.IoC;

namespace Tests;

public class StartCommandTests
{
    public StartCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterActionStart_MoveCommand_RegistersAndResolvesCorrectly()
    {
        var moveCmd = new Mock<ICommand>();
        var receiver = new Mock<ICommandReceiver>();
        var targetObject = new object();

        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Move",
            (object[] args) => moveCmd.Object).Execute();

        var registerActionsStart = new RegisterIoCDependencyActionsStart();
        registerActionsStart.Execute();

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            { "target", targetObject },
            { "command", "Commands.Move" },
            { "receiver", receiver.Object },
        };

        var startCommand = Ioc.Resolve<ICommand>("Actions.Start", new object[] { order });
        startCommand.Execute();

        Assert.NotNull(startCommand);
        Assert.IsType<StartCommand>(startCommand);

        moveCmd.Verify(c => c.Execute(), Times.Once);
        receiver.Verify(r => r.Receive(It.Is<ICommand>(c => ReferenceEquals(c, startCommand))), Times.Once);
    }
}
