using AutoMapper;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
using System.Collections.Generic;
using Manager.Struct.Exceptions;
using System.Threading.Tasks;
using Manager.Core.Queries.Users;
using Manager.Core.Types;

namespace Manager.Struct.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly ICrypton _crypton;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IScheduleRepository scheduleRepository,
            IAttendeeRepository attendeeRepository, ICrypton crypton, IMapper mapper)
        {
            _userRepository = userRepository;
            _scheduleRepository = scheduleRepository;
            _attendeeRepository = attendeeRepository;
            _crypton = crypton;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetSingleAsync(u => u.Email == email);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<PagedResult<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllPageable();
            return _mapper.Map<PagedResult<User>, PagedResult<UserDto>>(users);
        }

        public async Task<PagedResult<UserDto>> BrowseByProfessionAsync(BrowseUsersByProfession query)
        {
            var filterUsers = await _userRepository.GetAllPageable(u => u.Profession == query.Profession, query);
            return _mapper.Map<PagedResult<User>, PagedResult<UserDto>>(filterUsers);
        }

        public async Task<PagedResult<UserDto>> BrowseByRoleAsync(BrowseUsersByRole query)
        {
            var filterUsers = await _userRepository.GetAllPageable(u => u.Role == query.Role, query);
            return _mapper.Map<PagedResult<User>, PagedResult<UserDto>>(filterUsers);
        }

        public async Task<bool> FindUserInRoleAsync(int id)
        {
            var user = await _userRepository.GetSingleAsync(u => u.Id == id);
            var userInRole = user.Role != null;

            return userInRole;
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials,
                    "Invalid credentials");
            }

            var hash = _crypton.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }
            throw new ServiceException(ErrorCodes.InvalidCredentials,
                "Invalid credentials");
        }

        public async Task<UserDto> RegisterAsync(string name, string email, string fullName,
            string password,string avatar, string role, string profession)
        {
            var user = await _userRepository.GetAsync(name);

            if (user != null)
            {
                throw new ServiceException(ErrorCodes.NameInUse,
                    $"User with name: '{user.Name}' already exists.");
            }

            var salt = _crypton.GetSalt(password);
            var hash = _crypton.GetHash(password, salt);

            user = new User(name, email, fullName, hash, avatar, role, salt, profession);
            await _userRepository.AddAsync(user);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task UpdateUserAsync(int id, string name, string email, string fullName,
            string password, string avatar, string role, string profession)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidName,
                    $"User with id: {name} not exists.");
            }

            var salt = _crypton.GetSalt(password);

            user.SetName(name);
            user.SetEmail(email);
            user.SetFullName(fullName);
            user.SetPassword(password, salt);
            user.SetAvatar(avatar);
            user.SetRole(role);
            user.SetProfession(profession);          

            await _userRepository.UpdateAsync(user);
        }      

        public async Task RemoveUserScheduleAsync(int id)
        {
            var schedules = await _scheduleRepository.FindByAsync(s => s.CreatorId == id);

            foreach (var schedule in schedules)
            {
                _attendeeRepository.DeleteWhereAsync(a => a.Id == id);
                _scheduleRepository.DeleteAsync(schedule);
            }

            await _userRepository.Commit();
        }

        public async Task RemoveUserAttendeeAsync(int id)
        {
            var attendees = await _attendeeRepository.FindByAsync(a => a.Id == id);

            foreach (var attendee in attendees)
            {
                _attendeeRepository.DeleteAsync(attendee);
            }

            await _userRepository.Commit();
        }

        public async Task RemoveUserAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: {id} not exists.");
            }

            await RemoveUserAttendeeAsync(id);
            await RemoveUserScheduleAsync(id);

             _userRepository.DeleteAsync(user);

            await _userRepository.Commit();
        }
    }
}