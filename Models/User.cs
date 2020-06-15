namespace ConstellationWebApp.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  
    public partial class User : IdentityUser
    {
        /*public int UserID { get; set; }*/

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters and must only contain letters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters and must only contain letters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Bio cannot be longer than 2000 characters.")]
        public string Bio { get; set; }


        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(150)]
        public string Seeking { get; set; }


        /*[RegularExpression("([0-9a-zA-Z :\\-_!@$%^&*()])+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.psd|.PSD)$")]*/
        public string PhotoPath { get; set; }

        public string ResumePhotoPath { get; set; }

        // this REGEXP only ensure it is formated like and email; we must create an actual 
        // method to ensure that it is real

        public bool displayMyProfile { get; set; }

        public ICollection<IntrestedCandidate> IntrestedCandidates { get; set; }
        public ICollection<RecruiterPicks> Candidates { get; set; }
        public ICollection<RecruiterPicks> Recuiter { get; set; }
        public ICollection<StarredPosting> StarredPostings { get; set; }
        public ICollection<StarredUser> StarredOwner { get; set; }
        public ICollection<StarredUser> StarredUsers { get; set; }
        public ICollection<StarredProject> StarredProjects { get; set; }
        public ICollection<Posting> Postings { get; set; }
        public virtual ICollection<ContactLink> ContactLinks { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; }

    }

}


