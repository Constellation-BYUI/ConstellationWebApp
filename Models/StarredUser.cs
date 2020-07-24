using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class StarredUser
    {
        //Primary Key
        public int StarredUserID { get; set; }

        //User who is being starred Foriegn Key
        public string UserStarredID { get; set; }

        //User who is being starred navigation Property
        public  User StarredPerson { get; set; }

        //User who is starring the other person
        public string StarredOwnerID { get; set; }

        //user who is starring the other navigation property
        public  User StarOwner { get; set; }
    }
}
