namespace Manager.Struct.Commands.Users
{
    public class RemoveUserSchedules : ICommand
    {
        public int Id { get; set; }

        public RemoveUserSchedules(int id)
        {
            Id = id;
        }
    }
}