using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class AssignedStarredPostingData
    {
        public int PostingID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public bool Owned { get; set; }
        public ICollection<Posting> Postings { get; set; }
        public ICollection<User> Users { get; set; }


    }
}
