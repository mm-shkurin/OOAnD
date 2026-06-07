using Moq;
using App;
using App.Scopes;
using Game.Interfaces;
using Game.Models;
using Game.IoC;
using Game.Commands;

namespace Tests;

public class AuthorizerTests
{
    public AuthorizerTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void CheckPermission_GrantedPermission_ReturnsTrue()
    {
        var authorizer = new PrefixTreeAuthorizer();
        authorizer.Grant("user-1", "ship-1", "Fire");

        Assert.True(authorizer.CheckPermission("user-1", "ship-1", "Fire"));
    }

    [Fact]
    public void CheckPermission_NotGrantedPermission_ReturnsFalse()
    {
        var authorizer = new PrefixTreeAuthorizer();

        Assert.False(authorizer.CheckPermission("user-1", "ship-1", "Move"));
    }

    [Fact]
    public void Revoke_ExistingPermission_RemovesPermission()
    {
        var authorizer = new PrefixTreeAuthorizer();
        authorizer.Grant("user-1", "ship-1", "Fire");
        authorizer.Revoke("user-1", "ship-1", "Fire");

        Assert.False(authorizer.CheckPermission("user-1", "ship-1", "Fire"));
    }

    [Fact]
    public void Revoke_OnMissingUser_DoesNotThrow()
    {
        var authorizer = new PrefixTreeAuthorizer();

        var exception = Record.Exception(() => authorizer.Revoke("user-1", "ship-1", "Fire"));

        Assert.Null(exception);
    }

    [Fact]
    public void Revoke_OnMissingObject_DoesNotThrow()
    {
        var authorizer = new PrefixTreeAuthorizer();
        authorizer.Grant("user-1", "ship-1", "Fire");

        var exception = Record.Exception(() => authorizer.Revoke("user-1", "ship-2", "Fire"));

        Assert.Null(exception);
    }

    [Fact]
    public void Grant_SamePermission_IsIdempotent()
    {
        var authorizer = new PrefixTreeAuthorizer();
        authorizer.Grant("user-1", "ship-1", "Move");
        authorizer.Grant("user-1", "ship-1", "Move");

        Assert.True(authorizer.CheckPermission("user-1", "ship-1", "Move"));
    }

    [Fact]
    public void CheckPermission_DifferentPermissions_ChecksCorrectly()
    {
        var authorizer = new PrefixTreeAuthorizer();
        authorizer.Grant("user-1", "ship-1", "Fire");
        authorizer.Grant("user-1", "ship-1", "Move");

        Assert.True(authorizer.CheckPermission("user-1", "ship-1", "Fire"));
        Assert.True(authorizer.CheckPermission("user-1", "ship-1", "Move"));
        Assert.False(authorizer.CheckPermission("user-1", "ship-1", "Rotate"));
    }

    [Fact]
    public void Register_AuthorizerDependency_ResolvesCorrectly()
    {
        new RegisterIoCDependencyAuthorizer().Execute();

        var authorizer = Ioc.Resolve<IAuthorizer>("Game.Authorizer");
        Assert.IsType<PrefixTreeAuthorizer>(authorizer);

        ((PrefixTreeAuthorizer)authorizer).Grant("user-1", "ship-1", "Fire");
        Assert.True(authorizer.CheckPermission("user-1", "ship-1", "Fire"));
    }
}

