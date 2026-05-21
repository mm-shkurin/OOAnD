using Moq;
using App;
using App.Scopes;
using Game.Interfaces;
using Game.Models;
using Game.IoC;
using Game.Commands;

namespace Tests;

public class MacroIoCTests
{
    public MacroIoCTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependncy_MacroCommand_ShouldRegisterCorrectly()
    {
        var cmd1Mock = new Mock<ICommand>();
        var cmd2Mock = new Mock<ICommand>();

        var macro = Ioc.Resolve<ICommand>(
            "Commands.Macro",
            cmd1Mock.Object,
            cmd2Mock.Object
        );

        macro.Execute();

        Assert.IsType<MacroCommand>(macro);
        cmd1Mock.Verify(c => c.Execute(), Times.Once());
        cmd2Mock.Verify(c => c.Execute(), Times.Once());
    }
}

