using Xunit;
using Moq;
using Game.Models;
using Game.Commands;
using Game.Interfaces;

namespace Tests;

public class RotateCommandTests
{
    [Fact]
    public void RotateCommand_OrientationAndSpeedKnown_MovesCorrectly()
    {
        var mockRotatable = new Mock<IRotatable>();
        var originalOrientation = new Angle(1);
        var angularSpeed = new Angle(1);

        mockRotatable.SetupGet(x => x.Orientation).Returns(originalOrientation);
        mockRotatable.SetupGet(x => x.AngularSpeed).Returns(angularSpeed);

        Angle newOrientation = null;

        mockRotatable.SetupSet(x => x.Orientation = It.IsAny<Angle>())
                      .Callback<Angle>(a => newOrientation = a);

        var cmd = new RotateCommand(mockRotatable.Object);

        cmd.Execute();

        var expectedOrientation = new Angle(2);
        Assert.Equal(expectedOrientation, newOrientation);
        mockRotatable.VerifySet(x => x.Orientation = expectedOrientation, Times.Once);
    }

    [Fact]
    public void RotateCommand_ObjectHasNoOrientation_ThrowsException()
    {
        var mockRotatable = new Mock<IRotatable>();
        mockRotatable.SetupGet(x => x.Orientation).Throws<InvalidOperationException>();
        mockRotatable.SetupGet(x => x.AngularSpeed).Returns(new Angle(90));

        var command = new RotateCommand(mockRotatable.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void RotateCommand_ObjectHasNoAngularSpeed_ThrowsException()
    {
        var mockRotatable = new Mock<IRotatable>();
        mockRotatable.SetupGet(x => x.Orientation).Returns(new Angle(0));
        mockRotatable.SetupGet(x => x.AngularSpeed).Throws<InvalidOperationException>();

        var command = new RotateCommand(mockRotatable.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void RotateCommand_OrientationCannotBeSet_ThrowsException()
    {
        var mockRotatable = new Mock<IRotatable>();
        mockRotatable.SetupGet(x => x.Orientation).Returns(new Angle(0));
        mockRotatable.SetupGet(x => x.AngularSpeed).Returns(new Angle(1));
        mockRotatable.SetupSet(x => x.Orientation = It.IsAny<Angle>()).Throws<InvalidOperationException>();

        var command = new RotateCommand(mockRotatable.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}
