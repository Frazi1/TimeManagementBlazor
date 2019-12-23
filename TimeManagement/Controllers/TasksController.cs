using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;


namespace TimeManagement.Controlllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<List<TaskDto>> Get() => await taskService.GetAllTasksAsync();

        [HttpGet("filter")]
        public async Task<PagedList<TaskDto>> Get([ModelBinder(typeof(FilterModelBinder))] Filter filter)
            => await taskService.GetTasksAsync(filter);

        [HttpPost]
        public async Task<TaskDto> AddTask(TaskDto dto) => await taskService.AddTask(dto);

        [Route("{id}")]
        [HttpDelete]
        public async Task DeleteTask(int id) => await taskService.DeleteTask(id);

        [HttpPut]
        public async Task<TaskDto> UpdateTask(TaskDto dto) => await taskService.UpdateTask(dto);
    }
}
