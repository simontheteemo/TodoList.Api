using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using TodoList.Core.Interfaces.Clients;

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

        public async Task<SendMessageResponse> SendMessageToSQS(string message)
        {
            // Send a message to the queue
            SendMessageRequest request = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = message
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
    }
}