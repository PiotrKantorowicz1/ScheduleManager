using System.Threading.Tasks;

namespace Manager.Struct.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
