using System.Threading.Tasks;

namespace Manager.Struct.Services
{
    public interface IDataRefiller : IService
    {
         Task SeedAsync();
    }
}