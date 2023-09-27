using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.Repositories;

namespace TodoList.Core.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _repository;

        public TodoListService(ITodoListRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> CreateTodoItem(TodoItem item)
        {
            var isExisted = _repository.IsDescriptionExisted(item.Description);
            if (isExisted)
            {
                throw new Exception("Description already exists");
            }

            return await _repository.CreateTodoItem(item);
        }

        public async Task<TodoItem?> GetTodoItem(Guid itemId)
        {
            return await _repository.GetTodoItem(itemId);
        }

        public async Task<IList<TodoItem>> GetTodoItems()
        {   
            return await _repository.GetTodoItems();
        }

        public async Task<bool> UpdateTodoItem(TodoItem item)
        {
            return await _repository.UpdateTodoItem(item);
        }
    }
}
