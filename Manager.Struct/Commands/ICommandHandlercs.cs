using System.Threading.Tasks;

namespace Manager.Struct.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}