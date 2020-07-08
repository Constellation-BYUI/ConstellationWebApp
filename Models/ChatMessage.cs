using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class ChatMessage
    {
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
        public int MessageID { get; set; }
        public Message Message { get; set; }

    }
}
