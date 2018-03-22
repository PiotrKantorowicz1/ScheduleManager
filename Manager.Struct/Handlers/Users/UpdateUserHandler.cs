using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Users;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Users
{
    public class UpdateUserHandler : ICommandHandler<UpdateUser>
    {
        private readonly IUserService _userService;

        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UpdateUser command)
        {
            await _userService.UpdateUserAsync(command.Id, command.Name, command.FullName, command.Email,
                command.Avatar, command.Role, command.Avatar);
        }
    }
}