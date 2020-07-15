using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class Skill
    {
        public int SkillID { get; set; }

        public string SkillName { get; set; }

        public virtual ICollection<SkillDiscipline> SkillDisciplines { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; }

        public ICollection<ProjectSkills> ProjectSkills { get; set; }

        public ICollection<PostingSkills> PostingSkills { get; set; }


    }
}
