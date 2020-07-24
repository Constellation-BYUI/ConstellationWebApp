using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class ProjectSkills
    {
        public int SkillID { get; set; }
        public  Skill Skill { get; set; }
        public int ProjectID { get; set; }
        public  Project Project { get; set; }

    }
}
