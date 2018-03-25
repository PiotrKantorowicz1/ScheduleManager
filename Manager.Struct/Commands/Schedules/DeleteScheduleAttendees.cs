using System;
using Manager.Core.Models;

namespace Manager.Struct.Commands.Schedules
{
    public class DeleteScheduleAttendees : ICommand
    {
        public int Id { get; set; }
        public int AttendeeId { get; set; }
        
        public DeleteScheduleAttendees(int id, int attendeeId)
        {
            Id = id;
            AttendeeId = attendeeId;
        }
    }
}
