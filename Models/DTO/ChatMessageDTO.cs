using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class ChatMessageDTO
    {
        public int ChatID { get; set; }
        public int MessageID { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }

    }
}
