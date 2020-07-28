﻿

namespace ConstellationWebApp.Models
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Project
    {
         public int ProjectID { get; set; }

        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        public string PhotoPath { get; set; }
        public  ICollection<ProjectLink> ProjectLinks { get; set; }
        public  ICollection<UserProject> UserProjects { get; set; }
        public  ICollection<StarredProject> StarredProjects { get; set; }

        public  ICollection<ProjectPosting> ProjectPostings { get; set; }

        public  ICollection<ProjectSkills> ProjectSkills { get; set; }


    }

}

