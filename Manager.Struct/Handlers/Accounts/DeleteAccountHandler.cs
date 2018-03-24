using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Users
{
    public class DeleteAccountHandler : ICommandHandler<DeleteAccount>
    {
        private readonly IUserService _userService;

        public DeleteAccountHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(DeleteAccount command)
        {
            await _userService.RemoveUserAsync(command.Id);
        }
    }
}