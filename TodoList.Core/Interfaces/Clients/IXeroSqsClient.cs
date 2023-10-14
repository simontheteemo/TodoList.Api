using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Core.Interfaces.Clients
{
    public interface IXeroSqsClient
    {
        Task<SendMessageResponse> SendMessageToSQS(string message);

        Task<List<Message>> ReceiveMessagesAsync(int maxMessages, int waitTimeSeconds );
    }
}
