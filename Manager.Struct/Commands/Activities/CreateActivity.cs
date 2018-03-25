using System;
using Manager.Core.Models;

namespace Manager.Struct.Commands.Activities
{
    public class CreateActivity : ICommand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public int CreatorId { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
