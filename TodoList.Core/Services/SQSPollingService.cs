using Amazon.SQS.Model;
using Amazon.SQS;
using Microsoft.Extensions.Hosting;
using TodoList.Core.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using TodoList.Core.Repositories;
using TodoList.Core.Models;
using Newtonsoft.Json;
using System;

public class SQSPollingService : IHostedService, IDisposable
{
    //private readonly IXeroSqsClient _xeroSqsClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SQSPollingService> _logger;

    public SQSPollingService(IServiceScopeFactory scopeFactory, ILogger<SQSPollingService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start polling Messages from Xero SQS");
        Task.Run(() => PollMessagesAsync(cancellationToken));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Clean-up logic if needed
        return Task.CompletedTask;
    }

    private async Task PollMessagesAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _xeroSqsClient = scope.ServiceProvider.GetRequiredService<IXeroSqsClient>();

                var messages = await _xeroSqsClient.ReceiveMessagesAsync(10, 20);

                foreach (var message in messages)
                {
                    _logger.LogInformation($"Received message: {message.Body}");
                    try
                    {
                        var todoItem = JsonConvert.DeserializeObject<TodoItem>(message.Body);
                        var _repository = scope.ServiceProvider.GetRequiredService<ITodoListRepository>();
                        var response = await _repository.CreateTodoItem(todoItem);
                        if (response != null )
                        {
                            _logger.LogInformation($"Inserted| {message.Body}");
                             var deleteMessageResponse = await _xeroSqsClient.DeleteMessageAsync(message.ReceiptHandle, cancellationToken);
                            if (deleteMessageResponse != null)
                            {
                                _logger.LogInformation($"Deleted| {deleteMessageResponse.ToString}");
                            }
                        }
                    }
                    catch (Exception ex) {
                        var exception = $"{nameof(PollMessagesAsync)}|{ex}";
                        _logger.LogError(exception);
                        throw new Exception(exception);
                    }
                }
            }
            // Add any additional processing or delay logic as needed
        }
    }

    public void Dispose()
    {
        // Clean-up logic if needed
    }
}
