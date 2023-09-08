using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = default!;
}
