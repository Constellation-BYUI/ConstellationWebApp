using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class Chat
    {
        public int ChatID { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime LastActivity { get; set; }

        public string ChatTitle { get; set; }

        public  ICollection<ChatUser> ChatUsers { get; set; }
        public  ICollection<ChatMessage> ChatMessages { get; set; }

    }
}
