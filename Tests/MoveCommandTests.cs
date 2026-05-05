using Xunit;
using Moq;
using Game.Models;
using Game.Commands;
using Game.Interfaces;

namespace Tests;

public class MoveCommandTests
{
    [Fact]
    public void MoveCommand_PositionAndSpeedKnown_MovesCorrectly()
    {
        var movingObject = new Mock<IMovable>();
        var originalPosition = new Vector(12, 5);
        var speed = new Vector(-4, 1);
        movingObject.SetupGet(x => x.Position).Returns(originalPosition);
        movingObject.SetupGet(x => x.Speed).Returns(speed);

        Vector? newPosition = null;
        movingObject.SetupSet(x => x.Position = It.IsAny<Vector>())
                    .Callback<Vector>(v => newPosition = v);

        var cmd = new MoveCommand(movingObject.Object);

        cmd.Execute();

        var expectedPosition = new Vector(8, 6);
        Assert.Equal(expectedPosition, newPosition);
        movingObject.VerifySet(x => x.Position = expectedPosition, Times.Once);
    }

    [Fact]
    public void MoveCommand_ObjectHasNoPosition_ThrowsException()
    {
        var mockMovable = new Mock<IMovable>();
        mockMovable.SetupGet(x => x.Position).Throws<InvalidOperationException>();
        mockMovable.SetupGet(x => x.Speed).Returns(new Vector(-4, 1));

        var command = new MoveCommand(mockMovable.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void MoveCommand_ObjectHasNoSpeed_ThrowsException()
    {
        var mockMovable = new Mock<IMovable>();
        mockMovable.SetupGet(x => x.Position).Returns(new Vector(12, 5));
        mockMovable.SetupGet(x => x.Speed).Throws<InvalidOperationException>();

        var command = new MoveCommand(mockMovable.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void MoveCommand_PositionCannotBeSet_ThrowsException()
    {
        var mockMovable = new Mock<IMovable>();
        mockMovable.SetupGet(x => x.Position).Returns(new Vector(12, 5));
        mockMovable.SetupGet(x => x.Speed).Returns(new Vector(-4, 1));
        mockMovable.SetupSet(x => x.Position = It.IsAny<Vector>()).Throws<InvalidOperationException>();

        var command = new MoveCommand(mockMovable.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

}
