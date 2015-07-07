﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Models;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using UKSSDC.Migrations;


namespace UKSSDC.Services.Data
{
    public class UnitOfWork : DbContext
    {
        public DbSet<Place> Places { get; set; }

        public DbSet<PostCode> PostCodes { get; set; }

        public DbSet<PostCodePerimeter> PostCodePerimeters { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Road> Roads { get; set; }

        public UnitOfWork() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnitOfWork, Configuration>());
            Database.Initialize(false);
        }

    }
}
