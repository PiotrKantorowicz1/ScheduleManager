using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Users;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Users
{
    public class RemoveUserAttendeesHandler : ICommandHandler<RemoveUserAttendees>
    {
        private readonly IUserService _userService;

        public RemoveUserAttendeesHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RemoveUserAttendees command)
        {
            await _userService.DeleteUserAttendeesProperlyAsync(command.Id);
        }
    }
}