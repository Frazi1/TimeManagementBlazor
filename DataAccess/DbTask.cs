using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class DbTask
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public bool IsCompleted { get; set; }
    }
}