using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Users;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Users
{
    public class RemoveUserActivitiesHandler : ICommandHandler<RemoveUserActivities>
    {
        private readonly IUserService _userService;

        public RemoveUserActivitiesHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RemoveUserActivities command)
        {
            await _userService.RemoveUserActivitiesAsync(command.Id);
        }
    }
}