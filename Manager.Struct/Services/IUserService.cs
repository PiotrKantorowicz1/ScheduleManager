using Manager.Struct.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Core.Queries.Users;
using Manager.Core.Types;

namespace Manager.Struct.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(int id);
        Task<UserDto> GetByEmail(string email);
        Task<IEnumerable<UserDto>> BrowseUsersAsync();
        Task<PagedResult<UserDto>> GetAllPegeable();
        Task<PagedResult<UserDto>> FilterByProfession(BrowseUsersByProfession query);
        Task<PagedResult<UserDto>> FilterByRole(BrowseUsersByRole query);
        Task<bool> FindUserInRole(int id);
        Task LoginAsync(string email, string password);
        Task<UserDto> RegisterAsync(string name, string email, string fullName,
            string password,string avatar, string role, string profession);
        Task UpdateUserAsync(int id, string name, string email, string fullName,
            string password, string avatar, string role, string profession);
        Task RemoveUserSchedule(int id);
        Task RemoveUserAttendee(int id);
        Task RemoveUserAsync(int id);
    }
}