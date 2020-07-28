using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class ChatDTO
    {
        public int ChatID { get; set; }
        public string ChatTitle { get; set; }
        public DateTime LastActivity { get; set; }

        public string currentUserId { get; set; }

        public IEnumerable<ChatMessageDTO> ChatMessages { get; set; }
        public IEnumerable<ChatUserDTO> ChatUsers { get; set; }

    }
}
