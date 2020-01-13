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
        public async Task DeleteTask(int id) => await _taskService.DeleteTaskAsync(id);

        [HttpPut]
        public async Task<TaskDto> UpdateTask(TaskDto dto) => await _taskService.UpdateTaskAsync(dto);
    }
}
