using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class ChatUserDTO
    {
        public int ChatID { get; set; }
        public string UserID { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }

    }
}
