using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using UKSSDC.Migrations;
using UKSSDC.Models;
using log4net;

namespace UKSSDC.Services.Data
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (UnitOfWork)); 

        public DbSet<Place> Places { get; set; }

        public DbSet<Postcode> Postcodes { get; set; } 

        public DbSet<PostcodePerimeter> PostcodePerimeters { get; set; } //TODO: Remove Camelcase, untidy! 

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
            log4net.Config.XmlConfigurator.Configure();

            if (ex is DbEntityValidationException)
            {
                Logger.Info(ex);
            }
            else if (ex is DbUpdateException)
            {
                Logger.Warn(ex);
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
