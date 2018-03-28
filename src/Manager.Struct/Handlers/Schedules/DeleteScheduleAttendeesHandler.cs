using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Schedules;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Schedules
{
    public class DeleteScheduleAttendeesHandler : ICommandHandler<DeleteScheduleAttendees>
    {
        private readonly IScheduleService _scheduleService;

        public DeleteScheduleAttendeesHandler(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task HandleAsync(DeleteScheduleAttendees command)
        {
            await _scheduleService.DeleteAttendeesAsync(command.ScheduleId, command.AttendeeId);
        }
    }
}