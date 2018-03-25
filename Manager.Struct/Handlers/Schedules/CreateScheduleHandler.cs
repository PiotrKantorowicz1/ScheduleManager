using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Schedules;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Schedules
{
    public class CreateScheduleHandler : ICommandHandler<CreateSchedule>
    {
        private readonly IScheduleService _scheduleService;

        public CreateScheduleHandler(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task HandleAsync(CreateSchedule command)
        {
            await _scheduleService.CreateAsync(command.Id, command.Title, command.Description, command.TimeStart, 
                command.TimeEnd, command.Location, command.CreatorId, command.Type, command.Status);
        }
    }
}