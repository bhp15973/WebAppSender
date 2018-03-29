using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSender.Settings
{
    public class AzureTopicSettings
    {
        public string TopicName { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public int Timeout { get; set; }
    }
}
