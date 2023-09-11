using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;
using TodoList.Core.Services;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;

namespace TodoList.IntegrationTests
{
    public class TodoListServiceIntegrationTests
    {
        [Fact]
        public async Task CreateTodoItem_UniqueDescription_ReturnsTodoItem()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "IntegrationTestDatabase")
                .Options;

            using (var context = new TodoContext(options))
            {
                var repository = new TodoListRepository(context);
                var service = new TodoListService(repository);

                var todoItem = new TodoItem
                {
                    Id = Guid.NewGuid(),
                    Description = "Test Description"
                };

                // Act
                var result = await service.CreateTodoItem(todoItem);

                // Assert
                Assert.Equal(todoItem, result);
            }
        }
    }
}
