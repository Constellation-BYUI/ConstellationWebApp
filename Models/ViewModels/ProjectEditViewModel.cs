using ConstellationWebApp.Migrations;
using ConstellationWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models.ViewModels
{
    public class ProjectEditViewModel : ProjectCreateViewModel
    {
        public int ProjectID { get; set; }
        public string OldPhotoPath { get; set; }

        public Discipline currentDiscipline { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public ICollection<SkillDiscipline> SkillDisciplines { get; set; }
        public ICollection<Discipline> Disciplines { get; set; }

        public ICollection<ProjectSkills> ProjectSkills { get; set; }


    }
}
