using App;

namespace Game.IoC;

public class CreateMacroCommandStrategy(string commandSpec)
{
    public ICommand Resolve(object[] args)
    {
        string[] commandsNames = Ioc.Resolve<string[]>(commandSpec);
        var commands = commandsNames.Select(name => Ioc.Resolve<ICommand>(name, args)).ToArray();

        return Ioc.Resolve<ICommand>("Commands.Macro", new object[] { commands });
    }
}