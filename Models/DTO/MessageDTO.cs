using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class MessageDTO
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public string SenderID { get; set; }
        public string SenderName { get; set; }
        public string PhotoPath { get; set; }

        public string SentTime { get; set; }
    }
}
