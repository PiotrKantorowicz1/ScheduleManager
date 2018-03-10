using System;
using System.Collections.Generic;

namespace Manager.Core.Models
{
    public class Schedule
    {
        public Schedule()
        {
            Attendees = new List<Attendee>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public ScheduleType Type { get; set; }
        public ScheduleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        public ICollection<Attendee> Attendees { get; set; }
    }

    public enum ScheduleStatus : byte
    {
        Valid = 1,
        Cancelled = 2
    }

    public enum ScheduleType : byte
    {
        Work = 1,
        Coffee = 2,
        Doctor = 3,
        Shopping = 4,
        Other = 5
    }
}