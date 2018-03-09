using System;

namespace Manager.Struct.DTO
{
    public class ActivityDetailsDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public string Creator { get; set; }
        public int CreatorId { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


        public string[] Statuses { get; set; }
        public string[] Priorities { get; set; }
        public string[] Types { get; set; }
    }
}