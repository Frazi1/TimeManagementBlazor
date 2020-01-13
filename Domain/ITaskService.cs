using System.Threading.Tasks;

namespace Domain
{
    public interface ITaskService
    {
        Task<TaskDto> AddTaskAsync(TaskDto dto);
        Task DeleteTaskAsync(int id);
        Task<TaskDto> UpdateTaskAsync(TaskDto dto);
        Task<PagedList<TaskDto>> GetTasksAsync(Filter filter);
    }
}