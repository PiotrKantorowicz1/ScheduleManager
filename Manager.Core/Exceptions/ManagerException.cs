using System;

namespace Manager.Core.Exceptions
{
    public abstract class ManagerException : Exception
    {
        public string Code { get; }

        protected ManagerException()
        {
        }

        protected ManagerException(string code)
        {
            Code = code;
        }

        protected ManagerException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected ManagerException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected ManagerException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected ManagerException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}