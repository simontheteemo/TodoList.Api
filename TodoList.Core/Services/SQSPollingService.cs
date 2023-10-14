using Amazon.SQS.Model;
using Amazon.SQS;
using Microsoft.Extensions.Hosting;
using TodoList.Core.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;

public class SQSPollingService : IHostedService, IDisposable
{
    //private readonly IXeroSqsClient _xeroSqsClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SQSPollingService> _logger;

    public SQSPollingService(IServiceScopeFactory scopeFactory, ILogger<SQSPollingService> logger)
    {
        //_xeroSqsClient = xeroSqsClient;
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
