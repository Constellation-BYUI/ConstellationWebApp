﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class StarredProject
    {
        public int StarredProjectID { get; set; }
        public string UserID { get; set; }
        public  User User { get; set; }
        public int ProjectID { get; set; }
        public  Project Project { get; set; }
    }
}
