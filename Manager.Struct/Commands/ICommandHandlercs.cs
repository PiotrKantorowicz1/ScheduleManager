using System.Threading.Tasks;

namespace Manager.Struct.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}