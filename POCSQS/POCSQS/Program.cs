using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;

namespace POCSQS
{
    class Program
    {
        private const string ACCESS_KEY = "AKIAVCCYMO6ADHKPZVPX";
        private const string SECRET_KEY = "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";
        private const string SERVICE_QUEUE_URL = "https://sqs.us-east-2.amazonaws.com/348077717376/SqsMessage";


        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var amazonConfig = new AmazonSQSConfig();
            amazonConfig.ServiceURL = SERVICE_QUEUE_URL;
            amazonConfig.RegionEndpoint = RegionEndpoint.USEast2;

            var sqsClient = new AmazonSQSClient(ACCESS_KEY, SECRET_KEY, amazonConfig);

            do
            {
                var messages = await sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
                {
                    QueueUrl = SERVICE_QUEUE_URL,
                    WaitTimeSeconds = 5,
                    MaxNumberOfMessages = 1,
                });

                messages.Messages.ForEach(x =>
                {
                    Console.WriteLine(x.Body);

                    sqsClient.DeleteMessageAsync(new DeleteMessageRequest
                    {
                        QueueUrl = SERVICE_QUEUE_URL,
                        ReceiptHandle = x.ReceiptHandle
                    });
                });

            } while (!Console.KeyAvailable);

        }

    }
}
