using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Task = System.Threading.Tasks.Task;

namespace Domain
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

        private TaskDto ToTaskDto(DbTask dbTask)
        {
            return new TaskDto
            {
                    Id = dbTask.Id,
                    Name = dbTask.Name,
                    CreatedAt = dbTask.CreatedAt,
                    IsCompleted = dbTask.IsCompleted,
                    TimeSpent = dbTask.TimeSpent
            };
        }

        public Task<List<TaskDto>> GetAllTasksAsync()
        {
            List<TaskDto> list = DbContext.Tasks
                .Select(ToTaskDto)
                .ToList();
            return Task.FromResult(list);
        }

        public Task<TaskDto> AddTask(TaskDto dto)
        {
            var dbTask = new DbTask
            {
                Name = dto.Name,
                IsCompleted = dto.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                TimeSpent = dto.TimeSpent
            };

            DbContext.Tasks.Add(dbTask);
            DbContext.SaveChanges();
            return Task.FromResult(ToTaskDto(dbTask));
        }

        public Task DeleteTask(int id)
        {
            DbTask dbTask = FindTaskById(id);
            DbContext.Remove(dbTask);
            DbContext.SaveChanges();
            return Task.CompletedTask;
        }

        private DbTask FindTaskById(int id)
        {
            return DbContext.Tasks.First(t => t.Id == id);
        }

        public Task<TaskDto> UpdateTask(TaskDto dto)
        {
            var dbTask = FindTaskById(dto.Id);

            dbTask.IsCompleted = dto.IsCompleted;
            dbTask.Name = dto.Name;
            dbTask.TimeSpent = dto.TimeSpent;

            DbContext.SaveChanges();

            return Task.FromResult(ToTaskDto(dbTask));
        }
    }
}