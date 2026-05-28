using Xunit;
using Moq;
using Game.Commands;
using App;

namespace Tests;

public class MacroCommandTests
{
    [Fact]
    public void Exectute_MacroCommand_ExecutesAllCommands()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();

        var macro = new MacroCommand(new[] { cmd1.Object, cmd2.Object });

        macro.Execute();

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_MacroCommand_StopsOnException()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();
        var cmd3 = new Mock<ICommand>();

        cmd2.Setup(c => c.Execute()).Throws(new Exception());

        var macro = new MacroCommand(new[] { cmd1.Object, cmd2.Object, cmd3.Object });

        Assert.Throws<Exception>(() => macro.Execute());

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
        cmd3.Verify(c => c.Execute(), Times.Never);
    }
}
