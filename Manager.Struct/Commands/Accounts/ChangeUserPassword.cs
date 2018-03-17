namespace Manager.Struct.Commands.Accounts
{
    public class ChangeUserPassword : ICommand
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
