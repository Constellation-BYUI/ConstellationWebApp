using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstellationWebApp.Models
{
    public class StarredPosting
    {
        public int StarredPostingID { get; set; }
        public string UserID { get; set; }
        public  User User { get; set; }
        public int PostingID { get; set; }
        public  Posting Posting { get; set; }


    }
}
