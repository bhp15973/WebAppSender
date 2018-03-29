using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSender.Settings
{
    public class AzureSettings
    {
        public string QueueName { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public int Timeout { get; set; }
    }
}
