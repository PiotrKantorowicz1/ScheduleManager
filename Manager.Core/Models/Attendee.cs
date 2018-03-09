namespace Manager.Core.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}