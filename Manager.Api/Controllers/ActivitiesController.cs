using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Struct.DTO;

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

        public async Task<IActionResult> Get()
        {
            var activity = await _activityService.BrowseAsync();

            return Json(activity);
        }

        [HttpGet("{id}", Name = "GetTask")]
        public async Task<IActionResult> Get(int id)
        {
            var activity = await _activityService.GetAsync(id);

            if (activity == null)
            {
                NotFound();
            }

            return Json(activity);
        }

        [HttpGet("{id}/details", Name = "GetTaskDetails")]
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
        public async Task<IActionResult> Create(ActivityDto activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newActivity = await _activityService.CreateAsync(activity);

            return Json(newActivity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _activityService.EditAsync(id);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "RemoveTask")]
        public async Task<IActionResult> Delete(int id)
        {
            await _activityService.DeleteAsync(id);

            return NoContent();
        }
    }
}