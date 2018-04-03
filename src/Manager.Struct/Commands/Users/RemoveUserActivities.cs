namespace Manager.Struct.Commands.Users
{
    public class RemoveUserActivities : ICommand
    {
        public int Id { get; set; }

        public RemoveUserActivities(int id)
        {
            Id = id;
        }
    }
}