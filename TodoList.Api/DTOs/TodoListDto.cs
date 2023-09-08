using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs
{
    public class TodoListDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
