using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface ITodoListService
    {
        Task<TodoItem> CreateTodoItem(TodoItem item);
        Task<TodoItem?> GetTodoItem(Guid itemId);
        Task<IList<TodoItem>> GetTodoItems();
        Task<bool> UpdateTodoItem(TodoItem item);
    }
}
