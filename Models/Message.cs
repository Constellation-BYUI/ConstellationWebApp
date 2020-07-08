using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public string SenderID { get; set; }
        public DateTime SentTime { get; set; }
        public User Sender { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }

    }
}
