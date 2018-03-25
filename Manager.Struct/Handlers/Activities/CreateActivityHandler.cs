using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Activities;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Activities
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IActivityService _activityservice;

        public CreateActivityHandler(IActivityService activityService)
        {
            _activityservice = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            await _activityservice.CreateAsync(command.Id, command.Title, command.Description, command.TimeStart, 
                command.TimeEnd, command.Location, command.CreatorId, command.Type, command.Priority, command.Status);
        }
    }
}