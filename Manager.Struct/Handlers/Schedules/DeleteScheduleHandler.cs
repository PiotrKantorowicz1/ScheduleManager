using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Schedules;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Schedules
{
    public class DeleteScheduleHandler : ICommandHandler<DeleteSchedule>
    {
        private readonly IScheduleService _scheduleService;

        public DeleteScheduleHandler(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task HandleAsync(DeleteSchedule command)
        {
            await _scheduleService.DeleteAsync(command.Id);
        }
    }
}