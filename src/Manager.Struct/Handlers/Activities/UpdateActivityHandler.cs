using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Activities;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Activities
{
    public class UpdateActivityHandler : ICommandHandler<UpdateActivity>
    {
        private readonly IActivityService _activityservice;

        public UpdateActivityHandler(IActivityService activityService)
        {
            _activityservice = activityService;
        }

        public async Task HandleAsync(UpdateActivity command)
        {
            await _activityservice.UpdateAsync(command.Id, command.Title, command.Description, command.TimeStart, 
                command.TimeEnd, command.Location, command.CreatorId, command.Type, command.Priority, command.Status);
        }
    }
}