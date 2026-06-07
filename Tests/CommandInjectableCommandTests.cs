using Xunit;
using Moq;
using Game.Commands;
using App;
using Game.Interfaces;

namespace Tests;

public class CommandInjectableCommandTests
{
    [Fact]
    public void Execute_CommandInjected_ExecutesInjectedCommand()
    {
        var innerCommand = new Mock<ICommand>();
        var injectable = new CommandInjectableCommand();

        injectable.Inject(innerCommand.Object);
        injectable.Execute();

        innerCommand.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_NoCommandInjected_ThrowsInvalidOperationException()
    {
        var injectable = new CommandInjectableCommand();

        Assert.Throws<InvalidOperationException>(() => injectable.Execute());
    }
}
