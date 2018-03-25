namespace Manager.Struct.Commands.Users
{
    public class RemoveUserAttendees : ICommand
    {
        public int Id { get; set; }

        public RemoveUserAttendees(int id)
        {
            Id = id;
        }
    }
}