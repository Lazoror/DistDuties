using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DistDuties.Models;

namespace DistDuties.DAL
{
    public class DistContext : DbContext
    {
        public DistContext() : base("DistContext") { }

        public DbSet<Project> Projects { get; set; }

    }
}