using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasksAsync();
    Task<TaskDto> AddTask(TaskDto dto);
    Task DeleteTask(int id);
    Task<TaskDto> UpdateTask(TaskDto dto);
}