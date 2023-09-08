using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;
using TodoList.Core.Repositories;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure.Respositories
{
    public class TodoListRespository : ITodoListRepository
    {
        private readonly TodoContext _context;

        public TodoListRespository(TodoContext todoContext)
        {
            _context = todoContext;
        }

        public async Task<TodoItem> CreateTodoItem(TodoItem item)
        {   
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TodoItem?> GetTodoItem(Guid itemId)
        {
            return await _context.TodoItems.FindAsync(itemId);
        }

        public async Task<IList<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<bool> UpdateTodoItem(TodoItem item)
        {
            var existing = await _context.TodoItems.FindAsync(item.Id);
            if (existing is null)
                throw new DbUpdateConcurrencyException();

            _context.Entry(existing).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool IsDescriptionExisted(string description)
        {
            return  _context.TodoItems
                .Any(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);

        }
    }
}
