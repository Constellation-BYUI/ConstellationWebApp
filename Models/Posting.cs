using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstellationWebApp.Models
{
    public class Posting
    {
        public int PostingID { get; set; }

        [Display(Name = "Posting Title")]
        public string PostingTitle { get; set; }

        public string Description { get; set; }

        [Display(Name = "Posting for Project / Company")]
        public string PostingFor { get; set; }

        public User PostingOwner { get; set; }

        public ICollection<StarredPosting> StarredPostings { get; set; }
        public ICollection<IntrestedCandidate> IntrestedCandidates { get; set; }
        public ICollection<Posting_PostingType> Posting_PostingTypes { get; set; }


    }
}
