namespace Manager.Struct.Commands.Accounts
{
    public class Revoke : ICommand
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}