using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> AddTask(TaskDto dto);
        Task DeleteTask(int id);
        Task<TaskDto> UpdateTask(TaskDto dto);
    }
}
