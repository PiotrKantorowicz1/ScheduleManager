using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Accounts
{
    public class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IAccountService _accountService;

        public ChangePasswordHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(ChangePassword command)
        {
            await _accountService.ChangePasswordAsync(command.UserId, 
                command.CurrentPassword, command.NewPassword);
        }
    }
}