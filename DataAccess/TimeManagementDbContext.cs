using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TimeManagementDbContext: DbContext
    {
        public DbSet<DbTask> Tasks { get; set; }

        public TimeManagementDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}