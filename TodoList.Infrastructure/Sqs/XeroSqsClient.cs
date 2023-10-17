using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TodoList.Core.Interfaces.Clients;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Sqs
{
    public class XeroSqsClient: IXeroSqsClient
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;
        private readonly IConfiguration Configuration;

        public XeroSqsClient(IConfiguration configuration)
        {
            Configuration = configuration;
            var awsAccessKey = Configuration["AWS:AccessKey"];
            var awsSecretKey = Configuration["AWS:SecretKey"];
            var credentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
            // Initialize SQS client
            _sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.APSoutheast2); // Replace YourRegion with the appropriate region

            // Get the URL of your SQS queue
            _queueUrl = "https://sqs.ap-southeast-2.amazonaws.com/521323833085/xero-tax-return-sqs"; // Replace YourQueueUrl with the actual queue URL
        }

        public async Task<SendMessageResponse> SendMessageToSQS(TodoItem item)
        {
            // Send a message to the queue
            SendMessageRequest request = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = JsonConvert.SerializeObject(item)
        };

            var response = await _sqsClient.SendMessageAsync(request);
            return response;
        }

        public async Task<List<Message>> ReceiveMessagesAsync(int maxMessages = 10, int waitTimeSeconds = 20)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = maxMessages,
                WaitTimeSeconds = waitTimeSeconds
            };

            var response = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
            return response.Messages;
        }

        public async Task<DeleteMessageResponse> DeleteMessageAsync(string receiptHandle, CancellationToken cancellationToken = default)
        {
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = _queueUrl, // Provide the URL of your SQS queue
                ReceiptHandle = receiptHandle
            };

            var response = await _sqsClient.DeleteMessageAsync(deleteMessageRequest, cancellationToken);
            return response;
        }
    }
}