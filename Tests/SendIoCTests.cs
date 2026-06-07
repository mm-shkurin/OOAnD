using Moq;
using App;
using App.Scopes;
using Game.Commands;
using Game.Interfaces;
using Game.IoC;

namespace Tests;

public class SendIoCTests
{
    public SendIoCTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependency_SendCommand_ShouldRegisterAndResolveCorrectly()
    {
        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();

        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        var sendCommand = Ioc.Resolve<ICommand>(
            "Commands.Send",
            new object[] { mockCommand.Object, mockReceiver.Object }
        );

        sendCommand.Execute();

        Assert.IsType<SendCommand>(sendCommand);
        mockReceiver.Verify(r => r.Receive(mockCommand.Object), Times.Once);
    }
}