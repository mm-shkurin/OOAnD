using Game.Interfaces;

namespace Game.Commands;

public class MacroCommand : ICommand
{
    private readonly ICommand[] _commands;

    public MacroCommand(ICommand[] commands)
    {
        _commands = commands ?? throw new ArgumentNullException(nameof(commands));
    }

    public void Execute()
    {
        ExecuteRecursive(0);
    }

    private void ExecuteRecursive(int index)
    {
        if (index >= _commands.Length)
            return;

        _commands[index].Execute();

        ExecuteRecursive(index + 1);
    }
}