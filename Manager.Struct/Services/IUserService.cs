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
        Task<bool> FindUserInRole(int id);
        Task LoginAsync(string email, string password);
        Task<UserDto> RegisterAsync(string name, string email, string fullName,
            string password,string avatar, string role, string profession);
        Task UpdateUserAsync(int id, UserDto user);
        Task RemoveUserSchedule(int id);
        Task RemoveUserAttendee(int id);
        Task RemoveUserAsync(int id);
    }
}