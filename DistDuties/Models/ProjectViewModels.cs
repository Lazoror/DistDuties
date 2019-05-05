using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistDuties.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatorEmail { get; set; }
        public List<string> Team { get; set; }
    }
}