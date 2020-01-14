using System;
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
        
        public async Task<PagedList<TaskDto>> GetTasksAsync(Filter filter)
        {
            var dbList = await DbContext.Tasks
                .ApplyPagination(filter)
                .ToListAsync();
        
            var dtoList = dbList.Select(ToTaskDto).ToList();
            int allItemsCount = await DbContext.Tasks.CountAsync();

            return new PagedList<TaskDto>(allItemsCount, dtoList);
        }

        public async Task<TaskDto> AddTaskAsync(TaskDto dto)
        {
            var dbTask = new DbTask
            {
                Name = dto.Name,
                IsCompleted = dto.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                TimeSpent = dto.TimeSpent
            };

            DbContext.Tasks.Add(dbTask);
            await DbContext.SaveChangesAsync();
            
            return ToTaskDto(dbTask);
        }

        public async Task DeleteTaskAsync(int id)
        {
            DbTask dbTask = await FindTaskByIdAsync(id);
            DbContext.Remove(dbTask);
            
            await DbContext.SaveChangesAsync();
        }

        private async Task<DbTask> FindTaskByIdAsync(int id)
        {
            return await DbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id)
                   ?? throw new EntityNotFoundException<DbTask>(id);
        }

        public async Task<TaskDto> UpdateTaskAsync(TaskDto dto)
        {
            var dbTask = await FindTaskByIdAsync(dto.Id);

            dbTask.IsCompleted = dto.IsCompleted;
            dbTask.Name = dto.Name;
            dbTask.TimeSpent = dto.TimeSpent;

            DbContext.Update(dbTask);
            await DbContext.SaveChangesAsync();

            return ToTaskDto(dbTask);
        }
    }
}