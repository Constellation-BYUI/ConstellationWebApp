using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.DTO
{
    public class CreateChatDataDTO
    {
        public string[] selectedChatUsersInitalCreate { get; set; }
        public string StartingMessage { get; set; }
    }
}
