using System;
using Manager.Core.Models;

namespace Manager.Struct.Commands.Activities
{
    public class UpdateActivity : ICommand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public int CreatorId { get; set; }
        public ActivityType Type { get; set; }
        public ActivityPriority Priority { get; set; }
        public ActivityStatus Status { get; set; }
    }
}
