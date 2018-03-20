using System;
using System.Threading.Tasks;
using Manager.Core.Exceptions;
using NLog;

namespace Manager.Struct.Services
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnCustomError(Func<ManagerException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<ManagerException, Logger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, Logger, Task> onError, bool propagateException = false);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        Task ExecuteAsync();
    }
}