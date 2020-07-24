using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class ChatPageDTO
    {
        public IEnumerable<ChatDTO> Chats { get; set; }

        public IEnumerable<UserDTO> Users { get; set; }

    }
}
