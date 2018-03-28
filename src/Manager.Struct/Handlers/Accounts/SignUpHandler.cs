using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Accounts
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IAccountService _accountService;

        public SignUpHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(SignUp command)
        {
            await _accountService.SignUpAsync(command.SerialNumber, command.Name, command.FullName, command.Email,
                command.Password, command.Avatar, command.Profession, command.Role);
        }
    }
}