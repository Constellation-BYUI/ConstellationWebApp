using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class PostingType
    {
        public int PostingTypeID { get; set; }

        public string PostingTypeName { get; set; }

        public  ICollection<Posting_PostingType> Posting_PostingTypes { get; set; }

    }
}
