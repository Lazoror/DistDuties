﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DataAccess.DAL
{
    public class DistContext : DbContext
    {
        public DistContext() : base("DistContext") { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TeamMate> TeamMates { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<DistContext>(new CreateDatabaseIfNotExists<DistContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}