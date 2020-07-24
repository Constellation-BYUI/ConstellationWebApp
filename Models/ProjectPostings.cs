using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstellationWebApp.Models
{
    public class ProjectPosting
    {

        public int ProjectPostingID { get; set; }

        public int ProjectID { get; set; }

        public int PostingID { get; set; }

        public  Posting Posting { get; set; }

        public  Project Project { get; set; }


    }
}
