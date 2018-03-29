using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSender.Models;

namespace WebAppSender.Interfaces
{
    public interface ISenderTopicMessage
    {
        Task SendMessage(TopicMessage topicMessage);
    }
}
