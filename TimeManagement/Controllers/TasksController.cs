using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;


namespace TimeManagement.Controlllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("filter")]
        public async Task<PagedList<TaskDto>> Get([ModelBinder(typeof(FilterModelBinder))] Filter filter)
            => await _taskService.GetTasksAsync(filter);

        [HttpPost]
        public async Task<TaskDto> AddTask(TaskDto dto) => await _taskService.AddTaskAsync(dto);

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound($"Entity with id {e.Id} is not found");
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskDto dto)
        {
            try
            {
                return new OkObjectResult(await _taskService.UpdateTaskAsync(dto));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound($"Entity with id {e.Id} is not found");
            }
        }
    }
}
