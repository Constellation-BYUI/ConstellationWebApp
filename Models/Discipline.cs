using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstellationWebApp.Models
{
    public class Discipline
    {
        public int DisciplineID { get; set; }

        [StringLength(50, ErrorMessage = "DisciplineName cannot be longer than 50 characters.")]
        [Display(Name = "Discipline Name")]
        public String DisciplineName { get; set; }
        public  ICollection<SkillDiscipline> SkillDiscipline { get; set; }
    }
}
