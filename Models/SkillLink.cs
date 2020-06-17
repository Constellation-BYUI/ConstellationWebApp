using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class SkillLink
    {
        public int SkillLinkID { get; set; }

        public string SkillLinkUrl { get; set; }

        public string SkilLinkLabel { get; set; }
        public UserSkill UserSkills { get; set; }

    }
}
