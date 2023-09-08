/*using AutoFixture.Xunit2; // Include this namespace for AutoFixture support
using FakeItEasy;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.Repositories;
using TodoList.Core.Services;
using Xunit;


namespace TodoList.Core.Services.UnitTests;
public class TodoListServiceTests
{
    [Fact]
    public async Task GetTodoItem_RepositoryReturnsSuccess_ReturnsOperationResultWithTodoItem()
    {
        // Arrange
        Guid itemId = Guid.NewGuid();
        var fakeRepository = A.Fake<ITodoListRepository>();
        var expectedResult = new TodoItem { Id = itemId, Description = "Sample Todo", IsCompleted = false };
        var operationResult = new OperationResult<TodoItem>
        {   
            ErrorList = new List<string>(),
            Result = expectedResult
        };
        
        A.CallTo(() => fakeRepository.GetTodoItem(itemId)).Returns(Task.FromResult(operationResult));

        var todoListService = new TodoListService(fakeRepository);

        // Act
        var result = await todoListService.GetTodoItem(itemId);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(expectedResult, result.Result);
    }

    [Fact]
    public async Task GetTodoItem_RepositoryReturnsError_ReturnsOperationResultWithError()
    {
        // Arrange
        Guid itemId = Guid.NewGuid();
        var fakeRepository = A.Fake<ITodoListRepository>();
        var expectedError = "Repository error message";
        var operationResult = new OperationResult<TodoItem>
        {
            ErrorList = new List<string>(
                new string[] { expectedError }
            ),
        };
        A.CallTo(() => fakeRepository.GetTodoItem(itemId)).Returns(Task.FromResult(operationResult));

        var todoListService = new TodoListService(fakeRepository);

        // Act
        var result = await todoListService.GetTodoItem(itemId);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains(expectedError, result.ErrorList);
    }

    [Fact]
    public async Task UpdateTodoItem_RepositoryReturnsSuccess_ReturnsOperationResultWithTrue()
    {
        // Arrange
        var fakeRepository = A.Fake<ITodoListRepository>();
        var todoItem = new TodoItem { Id = Guid.NewGuid(), Description = "Simon's Dinner", IsCompleted = true };
        var expectedResult = true;
        var operationResult = new OperationResult<bool>
        {
            ErrorList = new List<string>(),
            Result = true
        };

        A.CallTo(() => fakeRepository.UpdateTodoItem(todoItem))
            .Returns(operationResult);

        var todoListService = new TodoListService(fakeRepository);

        // Act
        var result = await todoListService.UpdateTodoItem(todoItem);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(expectedResult, result.Result);
    }

    [Fact]
    public async Task UpdateTodoItem_RepositoryReturnsError_ReturnsOperationResultWithError()
    {
        // Arrange
        var fakeRepository = A.Fake<ITodoListRepository>();
        var todoItem = new TodoItem { Id = Guid.NewGuid(), Description="Simon's Dinner", IsCompleted = true  };
        var expectedError1 = "Repository error message1";
        var expectedError2 = "Repository error message2";
        var operationResult = new OperationResult<bool>
        {
            ErrorList = new List<string>(
                new string[] { expectedError1, expectedError2 }
            ),
        };

        A.CallTo(() => fakeRepository.UpdateTodoItem(todoItem))
            .Returns(operationResult);

        var todoListService = new TodoListService(fakeRepository);

        // Act
        var result = await todoListService.UpdateTodoItem(todoItem);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains(expectedError1, result.ErrorList);
        Assert.Contains(expectedError2, result.ErrorList);
    }

}
*/