using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class Posting_PostingType
    {
        public int Posting_PostingTypeID { get; set; }
        public int PostingID { get; set; }
        public int PostingTypeID { get; set; }
        //comment assigned once posting pull is made
        public bool Assigned { get; set; }
        public  Posting Postings { get; set; }
        public  PostingType PostingTypes { get; set; }


    }
}
