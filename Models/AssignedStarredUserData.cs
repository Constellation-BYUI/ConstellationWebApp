using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class AssignedStarredUserData
    {
        //User who is being starred Foriegn Key

        public string UserStarredID { get; set; }
        public string UserName { get; set; }
        public bool Assigned { get; set; }

        //User who is starring the other person
        public string StarredOwnerID { get; set; }
        public ICollection<User> StarredPerson { get; set; }
        public ICollection<User> StarOwner { get; set; }
    }
}
