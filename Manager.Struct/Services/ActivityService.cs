using System;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Models;
using Manager.Core.Queries.Activities;
using Manager.Core.Repositories;
using Manager.Core.Types;
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

        public async Task<PagedResult<ActivityDto>> BrowseAsync()
        {
            var activities = await _activityRepository.GetAllPageable();
            return _mapper.Map<PagedResult<Activity>, PagedResult<ActivityDto>>(activities);
        }

        public async Task<PagedResult<ActivityDto>> BrowseByCreatorAsync(BrowseActivitiesByCreator query)
        {
            var filtersActivities = await _activityRepository.GetAllPageable(a => a.CreatorId == query.CreatorId, query);
            return _mapper.Map<PagedResult<Activity>, PagedResult<ActivityDto>>(filtersActivities);
        }

        public async Task<PagedResult<ActivityDto>> BrowseByTitleAsync(BrowseActivitiesByTitle query)
        {
            var filtersActivities = await _activityRepository.GetAllPageable(a => a.Title == query.Title, query);
            return _mapper.Map<PagedResult<Activity>, PagedResult<ActivityDto>>(filtersActivities);
        }

        public async Task CreateAsync(int id, string title, string description, DateTime timestart, DateTime timeEnd, string location, int creatorId,
            ActivityType type, ActivityPriority priority, ActivityStatus status)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity != null)
            {
                throw new ServiceException(ErrorCodes.TaskNotFound,
                    $"Activity with this {title} already exists.");
            }

            activity = new Activity(title, description, timestart, timeEnd, location,
                creatorId, type, priority, status);
            await _activityRepository.AddAsync(activity);
        }

        public async Task UpdateAsync(int id, string title, string description, DateTime timeStart, DateTime timeEnd, string location,
            int creatorId, ActivityType type, ActivityPriority priority, ActivityStatus status)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
            {
                throw new ServiceException(ErrorCodes.TaskNotFound,
                    $"Activity with this id: {id} not exists.");
            }

            activity.SetTitle(title);
            activity.SetDescription(description);
            activity.SetTimeStart(timeStart);
            activity.SetTimeEnd(timeEnd);
            activity.SetLocation(location);
            activity.SetCreator(creatorId);
            activity.Type = type;
            activity.Priority = priority;
            activity.Status = status;

            await _activityRepository.UpdateAsync(activity);
        }
      
        public async Task DeleteAsync(int id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
            {
                throw new ServiceException(ErrorCodes.TaskNotFound,
                    $"task with this id: {id} not exists.");
            }

            await _activityRepository.DeleteAsync(activity);
        }
    }
}