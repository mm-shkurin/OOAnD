using Moq;
using App;
using App.Scopes;
using Game.Interfaces;
using Game.Models;
using Game.IoC;
using Game.Commands;

namespace Tests;

public class RotateIoCTests
{
    public RotateIoCTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependncy_RotateCommand_ShouldRegisterCorrectly()
    {
        var gameObject = new object();
        var mockRotatable = new Mock<IRotatable>();

        var originalOrientation = new Angle(0);
        var angularSpeed = new Angle(1);

        mockRotatable.SetupGet(x => x.Orientation).Returns(originalOrientation);
        mockRotatable.SetupGet(x => x.AngularSpeed).Returns(angularSpeed);

        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Adapters.IRotatable",
            (object[] args) => mockRotatable.Object
        ).Execute();

        var registerCommand = new RegisterIoCDependencyRotateCommand();

        registerCommand.Execute();
        var rotateCommand = Ioc.Resolve<ICommand>("Commands.Rotate", gameObject);
        rotateCommand.Execute();

        Assert.IsType<RotateCommand>(rotateCommand);
        mockRotatable.VerifySet(m => m.Orientation = originalOrientation + angularSpeed, Times.Once);
    }
}

