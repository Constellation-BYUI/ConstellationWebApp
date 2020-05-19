using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class Posting
    {
        public int PostingID { get; set; }

        public string PostingTitle { get; set; }

        public string Description { get; set; }

        public string PostingFor { get; set; }

        public User PostingOwner { get; set; }

        public ICollection<Posting_PostingType> Posting_PostingTypes { get; set; }


    }
}
