using Xunit;
using Moq;
using App;
using App.Scopes;
using Game.Commands;
using Game.Interfaces;
using Game.Models;
using Game.IoC;

namespace Tests;

public class MoveIoCTests
{
    public MoveIoCTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependncy_MoveCommand_ShouldRegisterCorrectly()
    {
        var gameObject = new object();
        var mockMovable = new Mock<IMovable>();

        var originalPosition = new Vector(0, 0);
        var speed = new Vector(2, 3);

        mockMovable.SetupGet(x => x.Position).Returns(originalPosition);
        mockMovable.SetupGet(x => x.Speed).Returns(speed);

        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Adapters.IMovable",
            (object[] args) => mockMovable.Object
        ).Execute();

        var registerCommand = new RegisterIoCDependencyMoveCommand();

        registerCommand.Execute();
        var moveCommand = Ioc.Resolve<ICommand>("Commands.Move", gameObject);
        moveCommand.Execute();

        Assert.IsType<MoveCommand>(moveCommand);
        mockMovable.VerifySet(m => m.Position = originalPosition + speed, Times.Once);
    }
}
