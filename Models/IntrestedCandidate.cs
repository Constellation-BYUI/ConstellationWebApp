using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class IntrestedCandidate
    {
        public int IntrestedCandidateID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int PostingID { get; set; }
        public Posting Posting { get; set; }
    }
}
