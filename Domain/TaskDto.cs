using System;
using Newtonsoft.Json;

namespace Domain
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore]
        public TimeSpan TimeSpent { get; set; }

        public string TimeSpentString
        {
            get => TimeSpent.ToString();
            set => TimeSpent = TimeSpan.Parse(value);
        }
        
        public bool IsCompleted { get; set; }
    }
}