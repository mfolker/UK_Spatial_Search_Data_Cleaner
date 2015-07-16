using System;
using System.Data.Entity;
using UKSSDC.Models;

namespace UKSSDC.Services.Data
{
    public interface IUnitOfWork : IDependency
    {
        DbSet<Place> Places { get; set; }
        DbSet<PostCode> PostCodes { get; set; }
        DbSet<PostCodePerimeter> PostCodePerimeters { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<Road> Roads { get; set; }
        DbSet<ImportProgress> ImportProgress { get; set; }

        void Save();

        void SaveASync();

    }
}