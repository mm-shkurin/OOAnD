using Moq;
using App;
using App.Scopes;
using Game.Interfaces;
using Game.Models;
using Game.IoC;
using Game.Commands;

namespace Tests;

public class MacroMoveRotateIoCTests
{
    public MacroMoveRotateIoCTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterMacroMoveRotate_MacroMove_ResolvesCorrectly()
    {
        var moveCmd1 = new Mock<ICommand>();
        var moveCmd2 = new Mock<ICommand>();

        new RegisterIoCDependencyMacroCommand().Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Specs.Move",
            (object[] args) => new[] { "Commands.Move1", "Commands.Move2" }).Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Move1",
            (object[] args) => moveCmd1.Object).Execute();
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Move2",
            (object[] args) => moveCmd2.Object).Execute();

        new RegisterIoCDependencyMacroMoveRotate().Execute();

        var macroMove = Ioc.Resolve<ICommand>("Macro.Move");
        macroMove.Execute();

        moveCmd1.Verify(c => c.Execute(), Times.Once);
        moveCmd2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void RegisterMacroMoveRotate_MacroRotate_ResolvesCorrectly()
    {
        var rotateCmd1 = new Mock<ICommand>();
        var rotateCmd2 = new Mock<ICommand>();

        new RegisterIoCDependencyMacroCommand().Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Specs.Rotate",
            (object[] args) => new[] { "Commands.Rotate1", "Commands.Rotate2" }).Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Rotate1",
            (object[] args) => rotateCmd1.Object).Execute();
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.Rotate2",
            (object[] args) => rotateCmd2.Object).Execute();

        new RegisterIoCDependencyMacroMoveRotate().Execute();
        var macroRotate = Ioc.Resolve<ICommand>("Macro.Rotate");
        macroRotate.Execute();

        rotateCmd1.Verify(c => c.Execute(), Times.Once);
        rotateCmd2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void RegisterMacroMoveRotate_NoSpecs_ThrowsException()
    {
        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Assert.Throws<Exception>(() => Ioc.Resolve<ICommand>("Macro.Move"));
        Assert.Throws<Exception>(() => Ioc.Resolve<ICommand>("Macro.Rotate"));
    }
}