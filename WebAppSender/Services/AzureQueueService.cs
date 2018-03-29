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
    public class AzureQueueService : ISenderMessage, IDisposable
    {
        private readonly IOptions<AzureSettings> _azureSettings;
        private readonly QueueClient _queueClient ;
        public AzureQueueService(IOptions<AzureSettings> azureSettings)
        {
            _azureSettings = azureSettings;
            _queueClient = new QueueClient(azureSettings.Value.ServiceBusConnectionString, azureSettings.Value.QueueName)
            {
                OperationTimeout = TimeSpan.FromSeconds(azureSettings.Value.Timeout)
            };
        }

        public async Task SendMessage(Models.QueueMessage queueMessage)
        {
            var objToSend = JsonConvert.SerializeObject(queueMessage);
            var message = new Message(Encoding.UTF8.GetBytes(objToSend));
           
            // Send the message to the queue.
            await _queueClient.SendAsync(message);
        }

        public async void Dispose()
        {
            await _queueClient.CloseAsync();
        }
    }
}
