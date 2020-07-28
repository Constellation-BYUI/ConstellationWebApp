using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class ChatUser
    {

        public string UserID { get; set; }
        public  User User { get; set; }
        public int ChatID { get; set; }
        public  Chat Chat { get; set; }
    }
}
