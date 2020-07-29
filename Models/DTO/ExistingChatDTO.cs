using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class ExistingChatDTO
    {
        public int ChatID { get; set; }
        public string[] selectedChatUsers { get; set; }
        public string ChatTitle { get; set; }
        public string chatMessage { get; set; }


    }
}
