using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class Message
    {
        public int MessageID { get; set; }

        [StringLength(550)]
        public string MessageText { get; set; }
        public string SenderID { get; set; }
        public DateTime SentTime { get; set; }
        public  User Sender { get; set; }
        public  ICollection<ChatMessage> ChatMessages { get; set; }

    }
}
