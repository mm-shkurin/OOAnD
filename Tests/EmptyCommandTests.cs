using Game.Commands;
using Xunit;

public class EmptyCommandTests
{
    [Fact]
    public void Execute_EmptyCommand_ShouldDoNothing()
    {
        new EmptyCommand().Execute();
    }
}