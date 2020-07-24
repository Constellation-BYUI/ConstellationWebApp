using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class RecruiterPicks
    {
        public int RecuiterPicksID { get; set; }

        public string ListTitle { get; set; }

        public string RecuiterID { get; set; }

        public string CandidateID { get; set; }

        public int PostingID { get; set; }

        public  User Recuiter { get; set; }

        public  Posting Posting { get; set; }

        public  User Candidate { get; set; }

    }
}
