using TodoList.Core.Models;

namespace TodoList.Core.Repositories;

public interface ITodoListRepository
{
    Task<TodoItem> CreateTodoItem(TodoItem item);
    Task<TodoItem?> GetTodoItem(Guid itemId);
    Task<IList<TodoItem>> GetTodoItems();
    Task<bool> UpdateTodoItem(TodoItem item);
    bool IsDescriptionExisted(string item);
}
