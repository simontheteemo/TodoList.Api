using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface IXeroSqsService
    {
        SendMessageResponse SendMessageToSQS(string message);       
    }
}