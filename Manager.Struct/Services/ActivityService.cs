using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
using Manager.Struct.Exceptions;

namespace Manager.Struct.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<ActivityDto> GetAsync(int id)
        {
            var activity = await _activityRepository.GetAsync(id);

            if (activity == null)
            {
                throw new ServiceException(ErrorCodes.TaskNotFound,
                    $"task with this id: {id} not exists.");
            }

            return _mapper.Map<Activity, ActivityDto>(activity);
        }

        public async Task<ActivityDetailsDto> GetDetailsAsync(int id)
        {
            var activity = await _activityRepository.GetSingleAsync(s => s.Id == id, s => s.Creator);

            if (activity == null)
            {
                throw new ServiceException(ErrorCodes.DetailsNotFound,
                    $"Details of task with this id: {id} not exists.");
            }

            var activityDetails = _mapper.Map<Activity, ActivityDetailsDto>(activity);

            return activityDetails;
        }

        public async Task<IEnumerable<ActivityDto>> BrowseAsync()
        {
            var activity = await _activityRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityDto>>(activity);
        }

        public async Task<ActivityDto> CreateAsync(ActivityDto activity)
        {
            var newActivity = _mapper.Map<ActivityDto, Activity>(activity);

            await _activityRepository.AddAsync(newActivity);

            return _mapper.Map<Activity, ActivityDto>(newActivity);
        }

        public async Task<ActivityDto> EditAsync(int id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
            {
                throw new ServiceException(ErrorCodes.TaskNotFound,
                  $"task with this id: {id} not exists.");
            }

            await _activityRepository.UpdateAsync(activity);

            return _mapper.Map<Activity, ActivityDto>(activity);
        }

        public async Task DeleteAsync(int id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
            {
                throw new ServiceException(ErrorCodes.TaskNotFound,
                    $"task with this id: {id} not exists.");
            }

            _activityRepository.DeleteAsync(activity);
        }
    }
}