using AutoMapper;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
using System.Collections.Generic;
using Manager.Struct.Exceptions;
using System.Threading.Tasks;

namespace Manager.Struct.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IScheduleRepository scheduleRepository,
            IAttendeeRepository attendeeRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _scheduleRepository = scheduleRepository;
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);

            return _mapper.Map<User, UserDto>(user);
        }

        public Task<UserDto> GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> BrowseUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public Task<IEnumerable<UserDto>> FilterByProfession(string profession)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> FilterByRole(string role)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserDto> FindUserRole(string role)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserDto> LoginAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserDto> RegisterAsync(int userId, string name, string email, string password, string avatar, string role, string salt,
            string profession)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserDto> UpdateUserAsync(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserDto> RegisterAsync(string name, string avatar, string profession)
        {
            var user = await _userRepository.GetAsync(name);

            if (user != null)
            {
                throw new ServiceException(ErrorCodes.NameInUse,
                    $"User with name: '{user.Name}' already exists.");
            }

            user = new User(name, avatar, profession);
            await _userRepository.AddAsync(user);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> EditAsync(int id, UserDto user)
        {
            var upgUser = await _userRepository.GetAsync(id);
            if (upgUser == null)
            {
                throw new ServiceException(ErrorCodes.InvalidName,
                    $"User with id: {id} not exists.");
            }

            upgUser.Name = user.Name;
            upgUser.Avatar = user.Avatar;
            upgUser.Profession = user.Profession;

            await _userRepository.UpdateAsync(upgUser);

            return _mapper.Map<User, UserDto>(upgUser);
        }

        public Task RemoveUserSchedule(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUserAttendee(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveUserAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: {id} not exists.");
            }

            var attendees = await _attendeeRepository.FindByAsync(a => a.Id == id);
            var schedules = await _scheduleRepository.FindByAsync(s => s.CreatorId == id);

            foreach (var attendee in attendees)
            {
                _attendeeRepository.DeleteAsync(attendee);
            }

            foreach (var schedule in schedules)
            {
                _attendeeRepository.DeleteWhereAsync(a => a.Id == id);
                _scheduleRepository.DeleteAsync(schedule);
            }

            _userRepository.DeleteAsync(user);
            await _userRepository.Commit();
        }
    }
}