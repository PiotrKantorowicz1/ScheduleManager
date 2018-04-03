using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Users
{
    public class DeleteAccountHandler : ICommandHandler<DeleteAccount>
    {
        private readonly IAccountService _accountService;

        public DeleteAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(DeleteAccount command)
        {
            await  _accountService.DeleteAccount(command.Id);
        }
    }
}