using System;

namespace Manager.Struct.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public Guid SerialNumber { get; set; }
    }
}