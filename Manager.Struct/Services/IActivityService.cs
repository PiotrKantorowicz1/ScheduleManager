using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IActivityService : IService
    {
        Task<ActivityDto> GetAsync(int id);
        Task<ActivityDetailsDto> GetDetailsAsync(int id);
        Task<IEnumerable<ActivityDto>> BrowseAsync();
        Task<ActivityDto> CreateAsync(ActivityDto activity);
        Task<ActivityDto> EditAsync(int id);
        Task DeleteAsync(int id);
    }
}