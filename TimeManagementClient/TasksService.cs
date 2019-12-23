using System.Collections.Generic;
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

        public async Task<List<TaskDto>> GetAllTasksAsync()
            => await _httpClient.GetJsonAsync<List<TaskDto>>("tasks");

        public async Task<TaskDto> AddTask(TaskDto dto)
            => await _httpClient.PostJsonAsync<TaskDto>("tasks", dto);

        public async Task DeleteTask(int id)
            => await _httpClient.DeleteAsync($"tasks/{id}");

        public async Task<TaskDto> UpdateTask(TaskDto dto)
            => await _httpClient.PutJsonAsync<TaskDto>("tasks", dto);

        public async Task<PagedList<TaskDto>> GetTasksAsync(Filter filter)
        {
            string uri = $"tasks/filter?skip={filter.Skip.ToString()}&take={filter.Take.ToString()}";
            return await _httpClient.GetJsonAsync<PagedList<TaskDto>>(uri);
        }
    }
}