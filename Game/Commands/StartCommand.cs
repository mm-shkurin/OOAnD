using Game.Interfaces;
using App;

namespace Game.Commands;

public class StartCommand : ICommand
{
    private readonly ICommand _longOperation;
    private readonly ICommandReceiver _receiver;

    public StartCommand(ICommand longOperation, ICommandReceiver receiver)
    {
        _longOperation = longOperation;
        _receiver = receiver;
    }

    public void Execute()
    {
        new MacroCommand(new ICommand[] { _longOperation, new SendCommand(this, _receiver) }
        ).Execute();
    }
}