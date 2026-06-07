using Moq;
using App;
using App.Scopes;
using Game.Interfaces;
using Game.Models;
using Game.IoC;
using Game.Commands;

namespace Tests;

public class CreateStrategyTests
{
    public CreateStrategyTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Create_MacroCommandStrategy_ExecutesCorrectly()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();
        var cmd3 = new Mock<ICommand>();

        cmd1.Setup(c => c.Execute());
        cmd2.Setup(c => c.Execute());
        cmd3.Setup(c => c.Execute());

        var registerMacroCommand = new RegisterIoCDependencyMacroCommand();
        registerMacroCommand.Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Specs.Test",
            (object[] args) => new string[] { "Command.Test1", "Command.Test2" }).Execute();

        Ioc.Resolve<ICommand>("IoC.Register", "Command.Test1",
            (object[] args) => cmd1.Object).Execute();
        Ioc.Resolve<ICommand>("IoC.Register", "Command.Test2",
            (object[] args) => cmd2.Object).Execute();
        Ioc.Resolve<ICommand>("IoC.Register", "Command.Test3",
            (object[] args) => cmd3.Object).Execute();

        var macro = new CreateMacroCommandStrategy("Specs.Test").Resolve(Array.Empty<object>());
        macro.Execute();

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
        cmd3.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void Resolve_Should_ThrowException_When_SpecificationNotFound()
    {
        var strategy = new CreateMacroCommandStrategy("Specs.None");

        Assert.Throws<Exception>(() => strategy.Resolve(Array.Empty<object>()));
    }
}