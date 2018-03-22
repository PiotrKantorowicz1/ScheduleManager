using System.Collections.Generic;
using Manager.Struct.DTO;
using System.Threading.Tasks;
using Manager.Core.Queries.Users;
using Manager.Core.Types;

namespace Manager.Struct.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(int id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<PagedResult<UserDto>> BrowseAsync();
        Task<PagedResult<UserDto>> BrowseByProfessionAsync(BrowseUsersByProfession query);
        Task<PagedResult<UserDto>> BrowseByRoleAsync(BrowseUsersByRole query);
        Task UpdateUserAsync(int id, string name, string email, string fullName,
           string avatar, string role, string profession);
        Task RemoveUserScheduleAsync(int id);
        Task RemoveUserAttendeeAsync(int id);
        Task RemoveUserAsync(int id);
    }
}