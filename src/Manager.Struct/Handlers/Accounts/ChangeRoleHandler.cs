using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Accounts
{
    public class ChangeRoleHandler : ICommandHandler<ChangeRole>
    {
        private readonly IAccountService _accountService;

        public ChangeRoleHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(ChangeRole command)
        {
            await _accountService.ChangeRoleAsync(command.Id, command.Role);
        }
    }
}