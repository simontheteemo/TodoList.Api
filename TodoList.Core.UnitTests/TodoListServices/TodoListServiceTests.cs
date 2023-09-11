using FakeItEasy;
using TodoList.Core.Models;
using TodoList.Core.Repositories;

namespace TodoList.Core.Services.UnitTests;
public class TodoListServiceTests
{
    [Fact]
    public async Task CreateTodoItem_UniqueDescription_ReturnsTodoItem()
    {
        // Arrange
        var repository = A.Fake<ITodoListRepository>();
        var todoListService = new TodoListService(repository);

        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = "Test Description"
        };

        A.CallTo(() => repository.IsDescriptionExisted(todoItem.Description)).Returns(false);
        A.CallTo(() => repository.CreateTodoItem(todoItem)).Returns(Task.FromResult(todoItem));

        // Act
        var result = await todoListService.CreateTodoItem(todoItem);

        // Assert
        Assert.Equal(todoItem, result);
    }

    [Fact]
    public async Task CreateTodoItem_DuplicateDescription_ThrowsException()
    {
        // Arrange
        var repository = A.Fake<ITodoListRepository>();
        var todoListService = new TodoListService(repository);

        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = "Test Description"
        };

        A.CallTo(() => repository.IsDescriptionExisted(todoItem.Description)).Returns(true);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => todoListService.CreateTodoItem(todoItem));
    }

    [Fact]
    public async Task GetTodoItem_ExistingItemId_ReturnsTodoItem()
    {
        // Arrange
        var repository = A.Fake<ITodoListRepository>();
        var todoListService = new TodoListService(repository);

        var itemId = Guid.NewGuid();
        var expectedTodoItem = new TodoItem
        {
            Id = itemId,
            Description = "Test Description"
        };

        A.CallTo(() => repository.GetTodoItem(itemId)).Returns(Task.FromResult(expectedTodoItem));

        // Act
        var result = await todoListService.GetTodoItem(itemId);

        // Assert
        Assert.Equal(expectedTodoItem, result);
    }
}
