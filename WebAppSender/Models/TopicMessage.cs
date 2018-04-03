using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSender.Models
{
    public class TopicMessage
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
