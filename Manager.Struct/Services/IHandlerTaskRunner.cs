using System;
using System.Threading.Tasks;

namespace Manager.Struct.Services
{
    public interface IHandlerTaskRunner 
    {
        IHandlerTask Run(Func<Task> runAsync);
    }
}