using System;

namespace Manager.Core.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        public ActivityType Type { get; set; }
        public ActivityPriority Priority { get; set; }
        public ActivityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public enum ActivityPriority : byte
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

    public enum ActivityStatus : byte
    {
        ToMake = 1,
        Done = 2
    }

    public enum ActivityType : byte
    {
        Work = 1,
        Programming = 2,
        Private = 3,
        Other = 4
    }
}