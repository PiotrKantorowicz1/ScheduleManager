using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Activities;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Activities
{
    public class DeleteActivityHandler : ICommandHandler<DeleteActivity>
    {
        private readonly IActivityService _activityservice;

        public DeleteActivityHandler(IActivityService activityService)
        {
            _activityservice = activityService;
        }

        public async Task HandleAsync(DeleteActivity command)
        {
            await _activityservice.DeleteAsync(command.Id);
        }
    }
}