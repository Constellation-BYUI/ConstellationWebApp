using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class AssignedProjectData
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public bool Assigned { get; set; }
        public int ProjectID { get; }
        public ICollection<Project> Projects { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
