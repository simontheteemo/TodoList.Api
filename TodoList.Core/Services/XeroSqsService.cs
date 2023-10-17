using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Interfaces.Clients;
using TodoList.Core.Models;

namespace TodoList.Core.Services
{
    public class XeroSqsService : IXeroSqsService
    {
        private readonly IXeroSqsClient _xeroSqsClient;

        public XeroSqsService(IXeroSqsClient xeroSqsClient)
        {
            _xeroSqsClient = xeroSqsClient;
        }

        public SendMessageResponse SendMessageToSQS(TodoItem item)
        {
            return _xeroSqsClient.SendMessageToSQS(item).Result;
        }
    }
}
