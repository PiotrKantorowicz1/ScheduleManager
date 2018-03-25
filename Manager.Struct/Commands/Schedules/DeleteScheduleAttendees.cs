using System;
using Manager.Core.Models;

namespace Manager.Struct.Commands.Schedules
{
    public class DeleteScheduleAttendees : ICommand
    {
        public int ScheduleId { get; set; }
        public int AttendeeId { get; set; }
        
        public DeleteScheduleAttendees(int scheduleId, int attendeeId)
        {
            ScheduleId = scheduleId;
            AttendeeId = attendeeId;
        }
    }
}
