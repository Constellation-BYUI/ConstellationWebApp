using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class PostingSkills
    {
        public int SkillID { get; set; }
        public Skill Skill { get; set; }
        public int PostingID { get; set; }
        public Posting Posting { get; set; }
        public int PriorityLevel { get; set; }
    }
}
