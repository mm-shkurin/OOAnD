using App;

namespace Game.Interfaces;

public interface ICommandReceiver
{
    void Receive(ICommand command);
}