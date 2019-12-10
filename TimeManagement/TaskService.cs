using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Task = System.Threading.Tasks.Task;

namespace Services
{
    public class TaskService
    {
        protected TimeManagementDbContext DbContext { get; }

        public TaskService(TimeManagementDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private List<TaskDto> _tasks = Enumerable
            .Range(1, 10)
            .Select(i => new TaskDto
            {
                Id = i,
                Name = $"Task {i}",
                CreatedAt = DateTime.UtcNow.Subtract(TimeSpan.FromHours(i)),
                IsCompleted = i % 2 == 0,
                TimeSpent = TimeSpan.FromHours(i)
            })
            .ToList();

        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            return DbContext.Tasks
                .Select(dbTask => new TaskDto
                {
                    Id = dbTask.Id,
                    Name = dbTask.Name,
                    CreatedAt = dbTask.CreatedAt,
                    IsCompleted = dbTask.IsCompleted,
                    TimeSpent = dbTask.TimeSpent
                })
                .ToList();
        }

        public async Task AddTask(TaskDto dto)
        {
            var dbTask = new DbTask
            {
                Name = dto.Name,
                IsCompleted = dto.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                TimeSpent = dto.TimeSpent
            };

            await DbContext.Tasks.AddAsync(dbTask);
            await DbContext.SaveChangesAsync();
        }
    }
}