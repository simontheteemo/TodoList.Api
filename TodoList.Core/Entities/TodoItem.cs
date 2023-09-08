using System.ComponentModel.DataAnnotations;

namespace TodoList.Core.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
