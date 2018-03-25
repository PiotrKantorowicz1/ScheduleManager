using System;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Models;
using Manager.Core.Queries.Schedules;
using Manager.Core.Repositories;
using Manager.Core.Types;
using Manager.Struct.DTO;
using Manager.Struct.EF;
using Manager.Struct.Exceptions;

namespace Manager.Struct.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IUserRepository userRepository,
            IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _userRepository = userRepository;
            _attendeeRepository = attendeeRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<PagedResult<ScheduleDto>> BrowseAsync()
        {
            var schedules = await _scheduleRepository.GetAllPageable();
            return _mapper.Map<PagedResult<Schedule>, PagedResult<ScheduleDto>>(schedules);
        }

        public async Task<PagedResult<ScheduleDto>> BrowseByCreatorAsync(BrowseSchedulesByCreator query)
        {
            var filterSchedules = await _scheduleRepository.GetAllPageable(s => s.CreatorId == query.CreatorId, query);
            return _mapper.Map<PagedResult<Schedule>, PagedResult<ScheduleDto>>(filterSchedules);
        }

        public async Task<PagedResult<ScheduleDto>> BrowseByTitleAsync(BrowseSchedulesByTitle query)
        {
            var filterSchedules = await _scheduleRepository.GetAllPageable(s => s.Title == query.Titile, query);
            return _mapper.Map<PagedResult<Schedule>, PagedResult<ScheduleDto>>(filterSchedules);
        }

        public async Task CreateAsync(int id, string title, string description, DateTime timestart, DateTime timeEnd, 
            string location, int creatorId, string type, string status)
        {
            var schedule = await _scheduleRepository.GetByAsync(id);
            if (schedule != null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"Schedule with this {title} already exists.");
            }

            schedule = new Schedule(title, description, timestart, timeEnd, location,
                creatorId);
            await _scheduleRepository.AddAsync(schedule);

            foreach (var attendee in schedule.Attendees)
            {
                schedule.Attendees.Add(new Attendee(schedule.Id, attendee.Id));
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string title, string description, DateTime timeStart, DateTime timeEnd, 
            string location, int creatorId, string type, string status)
        {
            var schedule = await _scheduleRepository.GetAsync(id);
            if (schedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"Schedule with this id: {id} not exists.");
            }

            schedule.SetTitle(title);
            schedule.SetDescription(description);
            schedule.SetTimeStart(timeStart);
            schedule.SetTimeEnd(timeEnd);
            schedule.SetLocation(location);
            schedule.SetCreator(creatorId);
            schedule.Type = type;
            schedule.Status = status;

            _scheduleRepository.Update(schedule);
            _attendeeRepository.DeleteWhere(a => a.ScheduleId == id);

            foreach (var attendee in schedule.Attendees)
            {
                await _attendeeRepository.AddAsync(new Attendee(id, attendee.Id));
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await _scheduleRepository.GetAsync(id);
            if (schedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedule with this id: {id} not exists.");
            }

            _attendeeRepository.DeleteWhere(a => a.ScheduleId == id);
            _scheduleRepository.Delete(schedule);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAttendeesAsync(int id, int attendeeId)
        {
            var schedule = await _scheduleRepository.GetByAsync(id);
            if (schedule == null)
            {
                throw new ServiceException(ErrorCodes.ScheduleNotFound,
                    $"schedule with this id: {id} not exists.");
            }

            _attendeeRepository.DeleteWhere(a => a.ScheduleId == id && a.UserId == attendeeId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}