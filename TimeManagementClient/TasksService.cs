using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Components;

namespace TimeManagementClient
{
    public class TasksService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TasksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TaskDto> AddTaskAsync(TaskDto dto)
            => await _httpClient.PostJsonAsync<TaskDto>("tasks", dto);

        public async Task DeleteTaskAsync(int id)
            => await _httpClient.DeleteAsync($"tasks/{id}");

        public async Task<TaskDto> UpdateTaskAsync(TaskDto dto)
            => await _httpClient.PutJsonAsync<TaskDto>("tasks", dto);

        public async Task<PagedList<TaskDto>> GetTasksAsync(Filter filter)
        {
            string uri = $"tasks/filter?skip={filter.Skip.ToString()}&take={filter.Take.ToString()}";
            return await _httpClient.GetJsonAsync<PagedList<TaskDto>>(uri);
        }
    }
}