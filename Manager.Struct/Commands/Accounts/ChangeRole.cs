namespace Manager.Struct.Commands.Accounts
{
    public class ChangeRole : ICommand
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}