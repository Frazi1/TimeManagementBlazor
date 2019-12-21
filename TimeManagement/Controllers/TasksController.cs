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

        [HttpPost]
        public async Task<TaskDto> AddTask(TaskDto dto) => await taskService.AddTask(dto);

        [HttpDelete]
        public async Task DeleteTask(int id) => await taskService.DeleteTask(id);

        [HttpPut]
        public async Task<TaskDto> UpdateTask(TaskDto dto) => await taskService.UpdateTask(dto);
    }
}
