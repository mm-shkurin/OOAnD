using Xunit;
using Moq;
using Game.Commands;
using Game.Interfaces;

namespace Tests;

public class MacroCommandTests
{
    [Fact]
    public void MacroCommand_Executes_All_Commands()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();

        var macro = new MacroCommand(new[] { cmd1.Object, cmd2.Object });

        macro.Execute();

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void MacroCommand_Stops_On_Exception()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();
        var cmd3 = new Mock<ICommand>();

        cmd2.Setup(c => c.Execute()).Throws(new Exception("fail"));

        var macro = new MacroCommand(new[] { cmd1.Object, cmd2.Object, cmd3.Object });

        Assert.Throws<Exception>(() => macro.Execute());

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
        cmd3.Verify(c => c.Execute(), Times.Never);
    }
}