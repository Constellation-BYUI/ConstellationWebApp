using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstellationWebApp.Models
{
    public class AssignedPostingData
    {

        public int PostingID { get; set; }

        [Display(Name = "Posting Title")]
        public string PostingTitle { get; set; }

        public User PostingOwner { get; set; }
    }
}
