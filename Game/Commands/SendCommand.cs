using App;
using Game.Interfaces;

namespace Game.Commands;

public class SendCommand(ICommand command, ICommandReceiver receiver) : ICommand
{
    public void Execute()
    {
        receiver.Receive(command);
    }
}