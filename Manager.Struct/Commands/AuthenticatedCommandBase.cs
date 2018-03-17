namespace Manager.Struct.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public int UserId { get; set; }
    }
}