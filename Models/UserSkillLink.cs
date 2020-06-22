using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class UserSkillLink
    {
        public int UserSkillLinkID { get; set; }
        public int LinkID { get; set; }
        public int UserSkillID { get; set; }
       
        public UserSkill UserSkills { get; set; }
        public SkillLink SkillLinks { get; set; }

    }
}
