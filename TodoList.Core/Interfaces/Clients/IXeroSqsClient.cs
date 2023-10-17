using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces.Clients
{
    public interface IXeroSqsClient
    {
        Task<SendMessageResponse> SendMessageToSQS(TodoItem item);

        Task<List<Message>> ReceiveMessagesAsync(int maxMessages, int waitTimeSeconds );

        Task<DeleteMessageResponse> DeleteMessageAsync(string receiptHandle, CancellationToken cancellationToken = default);
    }
}
