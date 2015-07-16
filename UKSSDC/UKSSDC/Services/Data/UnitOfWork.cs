using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.Entity.Validation;
using UKSSDC.Migrations;


namespace UKSSDC.Services.Data
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Place> Places { get; set; }

        public DbSet<PostCode> PostCodes { get; set; }

        public DbSet<PostCodePerimeter> PostCodePerimeters { get; set; } //TODO: Remove Camelcase, untidy! 

        public DbSet<Region> Regions { get; set; }

        public DbSet<Road> Roads { get; set; }

        public DbSet<ImportProgress> ImportProgress { get; set; }

        public UnitOfWork() : base("DefaultConnection")
        {
            //Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnitOfWork, Configuration>());
            Database.Initialize(false);
        }


        public void Save()
        {
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                
                HandleError(ex);
            }
        }

        public async void SaveASync()
        {
            try
            {
                int x = await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void HandleError(Exception ex)
        {
            //TODO: Add exception handling for failed database writes

            if (ex is DbEntityValidationException)
            {

                //throw new NotImplementedException();
            }
            else if (ex is DbUpdateException)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TODO: Finish regions

            #region ImportProgress

            modelBuilder.Entity<ImportProgress>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<ImportProgress>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<ImportProgress>()
                .Property(p => p.FileName)
                .IsRequired();

            #endregion

            #region Place

            #endregion

            #region Postcode

            #endregion

            #region PostcodePerimeter

            #endregion

            #region Region

            #endregion

            #region Roads

            #endregion
        }
    }
}
