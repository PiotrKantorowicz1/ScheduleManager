using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Users;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Users
{
    public class RemoveUserSchedulesHandler : ICommandHandler<RemoveUserSchedules>
    {
        private readonly IUserService _userService;

        public RemoveUserSchedulesHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RemoveUserSchedules command)
        {
            await _userService.RemoveUserSchedulesAsync(command.Id);
        }
    }
}