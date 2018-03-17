using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Accounts
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        private readonly IAccountService _accountService;

        public ChangeUserPasswordHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(ChangeUserPassword command)
        {
            await _accountService.ChangePasswordAsync(command.UserId, 
                command.CurrentPassword, command.NewPassword);
        }
    }
}