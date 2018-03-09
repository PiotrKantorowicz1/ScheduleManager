using Manager.Struct.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Struct.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(int id);
        Task<UserDto> GetByEmail(string email);
        Task<IEnumerable<UserDto>> BrowseUsersAsync();
        Task<IEnumerable<UserDto>> FilterByProfession(string profession);
        Task<IEnumerable<UserDto>> FilterByRole(string role);
        Task<UserDto> FindUserRole(string role);
        Task<UserDto> LoginAsync(string email, string password);
        Task<UserDto> RegisterAsync(int userId, string name, string email, string password,
            string avatar, string role, string salt, string profession);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task RemoveUserSchedule(int id);
        Task RemoveUserAttendee(int id);
        Task RemoveUserAsync(int id);
    }
}