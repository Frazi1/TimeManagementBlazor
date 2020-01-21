using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            var uriBuilder = new StringBuilder("tasks/filter");

            var ops = new List<string>(3);
            if (filter.Skip.HasValue)
                ops.Add($"skip={filter.Skip.ToString()}");
            if (filter.Take.HasValue)
                ops.Add($"take={filter.Take.ToString()}");
            if (filter.Sortings.Any())
            {
                var sort = filter.Sortings[0];
                string sortDirection = sort.IsAscending ? "asc" : "desc";
                ops.Add($"order_by={sort.PropertyName},{sortDirection}");
            }

            if (ops.Any())
                uriBuilder.Append("?");
            uriBuilder.Append(string.Join("&", ops));

            return await _httpClient.GetJsonAsync<PagedList<TaskDto>>(uriBuilder.ToString());
        }
    }
}