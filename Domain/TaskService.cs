using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Domain
{
    public class TaskService : ITaskService
    {
        protected TimeManagementDbContext DbContext { get; }

        public TaskService(TimeManagementDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private static TaskDto ToTaskDto(DbTask dbTask)
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

        public async Task<PagedList<TaskDto>> GetTasksAsync(Filter filter)
        {
            var dbList = await DbContext.Tasks
                .ApplyPagination(filter)
                .ToListAsync();
            var dtoList = dbList.Select(ToTaskDto).ToList();

            int allItemsCount = await DbContext.Tasks.CountAsync();
            
            return new PagedList<TaskDto>(allItemsCount, dtoList);
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