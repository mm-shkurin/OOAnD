using Xunit;
using Moq;
using App;
using Game.Commands;
using Game.Interfaces;

namespace Tests;

public class SendCommandTests
{
    [Fact]
    public void SendCommand_PassesLongRunningCommandToReceiver()
    {
        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        sendCommand.Execute();

        mockReceiver.Verify(r => r.Receive(mockCommand.Object), Times.Once);
    }

    [Fact]
    public void SendCommand_ThrowsExceptionWhenReceiverCannotAcceptCommand()
    {
        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();
        var expectedException = new InvalidOperationException("Receiver unavailable");
        mockReceiver.Setup(r => r.Receive(It.IsAny<ICommand>())).Throws(expectedException);

        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        var exception = Assert.Throws<InvalidOperationException>(() => sendCommand.Execute());
        Assert.Same(expectedException, exception);
    }
}