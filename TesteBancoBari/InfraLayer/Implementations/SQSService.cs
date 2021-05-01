using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TesteBancoBari.InfraLayer.Abstractions;
using TesteBancoBari.InfraLayer.Models;
using TesteBancoBari.CrossCutting.Models;

namespace TesteBancoBari.InfraLayer.Implementations
{
    public class SQSService<T> : ISQSService<T> where T : class
    {
        private readonly AmazonSQSClient _sqsClient;
        private readonly string _serviceQueueUrl;

        public SQSService(IConfiguration config)
        {
            _serviceQueueUrl = config["AmazonSQS:ServiceQueueUrl"];
            _sqsClient = GetSQSClient(
                config["AmazonSQS:AccessKey"],
                config["AmazonSQS:SecretKey"],
                _serviceQueueUrl);
        }

        private AmazonSQSClient GetSQSClient(string accessKey, string secretKey, string serviceQueueURL)
        {
            var amazonConfig = new AmazonSQSConfig();
            amazonConfig.ServiceURL = serviceQueueURL;
            amazonConfig.RegionEndpoint = RegionEndpoint.USEast2;

            return new AmazonSQSClient(accessKey, secretKey, amazonConfig);
        }

        public async Task DeleteItem(string itemId) =>
            await _sqsClient.DeleteMessageAsync(new DeleteMessageRequest
            {
                QueueUrl = _serviceQueueUrl,
                ReceiptHandle = itemId
            });

        public async Task<IEnumerable<ReceiveMessageModel>> ReceiveItem()
        {
            var responseMessage = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                WaitTimeSeconds = 10,
                MaxNumberOfMessages = 10,
                QueueUrl = _serviceQueueUrl
            });

            if (responseMessage.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return responseMessage.Messages.Select(x => new ReceiveMessageModel
                {
                    Body = x.Body,
                    Id = x.MessageId,
                });
            }
            else
            {
                return null;
            } 
        }

        public async Task<AddItemResponseModel> SendItem(T bodyItem)
        {
            var bodyItemJson = JsonSerializer.Serialize(bodyItem);

            var response = await _sqsClient.SendMessageAsync(new SendMessageRequest
            {
                MessageBody = bodyItemJson,
                QueueUrl = _serviceQueueUrl,
            });

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return new AddItemResponseModel
                {
                    Id = response.MessageId
                };
            else
                return null;
        }
    }
}
