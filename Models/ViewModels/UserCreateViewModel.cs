using ConstellationWebApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstellationWebApp.ViewModels
{
    public class UserCreateViewModel
    {
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters and must only contain letters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters and must only contain letters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(2000, ErrorMessage = "Bio cannot be longer than 2000 characters.")]
        public string Bio { get; set; }

        [StringLength(300)]
        public string Seeking { get; set; }
        public string PhotoPath { get; set; }

        public IFormFile Photo { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
    }

}