using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Queue.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppSender.Interfaces;
using WebAppSender.Models;
using WebAppSender.Settings;

namespace WebAppSender.Services
{
    public class AzureTopicService : ISenderTopicMessage, IDisposable
    {
        private readonly IOptions<AzureTopicSettings> _azureSettings;
        private readonly TopicClient _topicClient;
        public AzureTopicService(IOptions<AzureTopicSettings> azureSettings)
        {
            _azureSettings = azureSettings;
            _topicClient = new TopicClient(azureSettings.Value.ServiceBusConnectionString,
                azureSettings.Value.TopicName)
            {
                OperationTimeout = TimeSpan.FromSeconds(azureSettings.Value.Timeout),
            };
        }

        public async Task SendMessage(TopicMessage topicMessage)
        {
            var objToSend = JsonConvert.SerializeObject(topicMessage);
            var message = new Message(Encoding.UTF8.GetBytes(objToSend));
           
            // Send the message to the queue.
            await _topicClient.SendAsync(message);
        }

        public async void Dispose()
        {
            await _topicClient.CloseAsync();
        }
    }
}
