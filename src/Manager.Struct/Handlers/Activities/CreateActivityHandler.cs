using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Activities;
using Manager.Struct.Services;
using NLog;

namespace Manager.Struct.Handlers.Activities
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IActivityService _activityservice;
        private readonly IHandler _handler;

        public CreateActivityHandler(IActivityService activityService, IHandler handler)
        {
            _activityservice = activityService;
            _handler = handler;
        }

        //Task runners test
        public async Task HandleAsync(CreateActivity command)
            => await _handler.Run(async () =>           
                {
                    await _activityservice.CreateAsync(command.Id, command.Title, command.Description, command.TimeStart,
                        command.TimeEnd, command.Location, command.CreatorId, command.Type, command.Priority, command.Status);
                })    
                .ExecuteAsync();

    }
}