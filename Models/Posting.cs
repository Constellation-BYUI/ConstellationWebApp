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

        [Required]
        [Display(Name = "Posting Title")]
        public string PostingTitle { get; set; }

        [Required]

        public string Description { get; set; }

        [Required]
        [Display(Name = "Posting for Project / Company")]
        public string PostingFor { get; set; }

        public User PostingOwner { get; set; }

        [Display(Name = "Allow your Teams on Costellation to Use this Posting")]
        public bool SharableToTeam { get; set; }

        [Display(Name = "Hide this Posting")]
        //A recuiter may want to recuiter privately or may want to turn off the posting for a tim
        //but want to have it for later
        public bool  HidePosting { get; set; }

        public ICollection<StarredPosting> StarredPostings { get; set; }
        public ICollection<IntrestedCandidate> IntrestedCandidates { get; set; }
        public ICollection<Posting_PostingType> Posting_PostingTypes { get; set; }

        public ICollection<RecruiterPicks> RecruiterPicks { get; set; }

        public ICollection<ProjectPosting> ProjectPostings { get; set; }


    }
}
