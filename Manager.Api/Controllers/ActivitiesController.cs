using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Queries.Activities;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        [Route("GetAllPageable")]
        public async Task<IActionResult> Get()
        {
            var activity = await _activityService.GetAllPageable();

            return Json(activity);
        }

        [HttpGet]
        [Route("FilterByCreator/{creatorId}")]
        public async Task<IActionResult> Get(BrowseActivitiesByCreator query)
        {
            var users = await _activityService.FilterByCreator(query);

            return Json(users);
        }

        [HttpGet]
        [Route("FilterByTitle/{title}")]
        public async Task<IActionResult> Get(BrowseActivitiesByTitle query)
        {
            var users = await _activityService.FilterByTitle(query);

            return Json(users);
        }

        [HttpGet]
        [Route("FilterByLocation/{location}")]
        public async Task<IActionResult> Get(BrowseActivitiesByLocation query)
        {
            var users = await _activityService.FilterByLocation(query);

            return Json(users);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var activity = await _activityService.GetAsync(id);

            if (activity == null)
            {
                NotFound();
            }

            return Json(activity);
        }

        [HttpGet]
        [Route("{id}/Details")]
        public async Task<IActionResult> GetTaskDetails(int id)
        {
            var activityDetails = await _activityService.GetDetailsAsync(id);

            if (activityDetails == null)
            {
                NotFound();
            }

            return Json(activityDetails);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Activity activity)
        {
            await _activityService.CreateAsync(activity.Id, activity.Title, activity.Description, activity.TimeStart,
                activity.TimeEnd, activity.Location,activity.CreatorId, activity.Type, activity.Priority, activity.Status);

            return Created($"users/{activity.Title}", null);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Activity activity)
        { 
            await _activityService.UpdateAsync(id, activity.Title, activity.Description, activity.TimeStart, activity.TimeEnd,
                activity.Location, activity.CreatorId, activity.Type, activity.Priority, activity.Status);

            return NoContent();
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _activityService.DeleteAsync(id);

            return NoContent();
        }
    }
}