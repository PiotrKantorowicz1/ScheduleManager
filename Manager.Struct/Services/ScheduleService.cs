using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
using Manager.Struct.Exceptions;

namespace Manager.Struct.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IUserRepository userRepository,
            IAttendeeRepository attendeeRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _userRepository = userRepository;
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> GetAsync(int id)
        {
            var schedule = await _scheduleRepository.GetAsync(id);

            if (schedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedule with this id: {id} not exists.");
            }

            return _mapper.Map<Schedule, ScheduleDto>(schedule);
        }

        public async Task<ScheduleDetailsDto> GetScheduleDetailsAsync(int id)
        {
            var schedule = await _scheduleRepository.GetAsync(id);

            if (schedule == null)
            {
                throw new ServiceException(ErrorCodes.DetailsNotFound,
                    $"Details of schedule with this id: {id} not exists.");
            }

            var scheduleDetails = _mapper.Map<Schedule, ScheduleDetailsDto>(schedule);

            foreach (var attendee in schedule.Attendees)
            {
                var user = await _userRepository.GetAsync(attendee.UserId);
                scheduleDetails.Attendees.Add(_mapper.Map<User, UserDto>(user));
            }

            return scheduleDetails;
        }

        public async Task<IEnumerable<ScheduleDto>> BrowseAsync()
        {
            var schedules = await _scheduleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleDto>>(schedules);
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesAsync(int id)
        {
            var userSchedules = await _scheduleRepository.FindByAsync(s => s.CreatorId == id);
            if (userSchedules == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedules with this ids: {id} not exists.");
            }

            return _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleDto>>(userSchedules);
        }

        public async Task<ScheduleDto> CreateAsync(ScheduleDto schedule)
        {
            var newSchedule = _mapper.Map<ScheduleDto, Schedule>(schedule);
            newSchedule.CreatedAt = DateTime.UtcNow;

            await _scheduleRepository.AddAsync(newSchedule);

            foreach (var userId in schedule.Attendees)
            {
                newSchedule.Attendees.Add(new Attendee {UserId = userId});
            }

            return _mapper.Map<Schedule, ScheduleDto>(newSchedule);
        }

        public async Task<ScheduleDto> EditAsync(int id)
        {
            var _schedule = await _scheduleRepository.GetAsync(id);
            if (_schedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedule with this id: {id} not exists.");
            }

            await _scheduleRepository.UpdateAsync(_schedule);
            _attendeeRepository.DeleteWhereAsync(a => a.ScheduleId == id);

            foreach (var userId in _schedule.Attendees)
            {
                await _attendeeRepository.AddAsync(userId);
            }

            return _mapper.Map<Schedule, ScheduleDto>(_schedule);
        }

        public async Task DeleteAsync(int id)
        {
            var removeSchedule = await _scheduleRepository.GetByAsync(id);
            if (removeSchedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedule with this id: {id} not exists.");
            }

            _attendeeRepository.DeleteWhereAsync(a => a.ScheduleId == id);
            _scheduleRepository.DeleteAsync(removeSchedule);
        }

        public async Task DeleteAttendeesAsync(int id, int attendee)
        {
            var removeSchedule = await _scheduleRepository.GetByAsync(id);
            if (removeSchedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedule with this id: {id} not exists.");
            }

            _attendeeRepository.DeleteWhereAsync(a => a.ScheduleId == id && a.UserId == attendee);
        }
    }
}