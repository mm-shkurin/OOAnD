using App;

namespace Game.Commands;

public class MacroCommand(ICommand[] commands) : ICommand
{
    public void Execute()
    {
        commands.ToList().ForEach(c => c.Execute());
    }
}