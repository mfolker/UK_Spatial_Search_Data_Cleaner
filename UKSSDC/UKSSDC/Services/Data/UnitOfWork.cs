using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using log4net;
using log4net.Config;
using UKSSDC.Migrations;
using UKSSDC.Models;

namespace UKSSDC.Services.Data
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (UnitOfWork)); 

        public DbSet<Place> Places { get; set; }

        public DbSet<Postcode> Postcodes { get; set; } 

        public DbSet<PostcodePerimeter> PostcodePerimeters { get; set; } 

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

        public override int SaveChanges()
        {
            DateTime now = DateTime.UtcNow;
            foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (!entry.IsRelationship)
                {
                    Common updated = entry.Entity as Common;
                    if (updated != null)
                        updated.Updated = now;
                }
            }

            foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                if (!entry.IsRelationship)
                {
                    Common created = entry.Entity as Common;
                    if (created != null)
                        created.Created = now;
                }
            }

            return base.SaveChanges();
        }

        private void HandleError(Exception ex)
        {
            XmlConfigurator.Configure(); 

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

            modelBuilder.Entity<Place>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<Place>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion

            //TODO: Determine which WKT's should be unique and apply constraint. 

            #region Postcode

            modelBuilder.Entity<Postcode>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<Postcode>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion

            #region PostcodePerimeter

            modelBuilder.Entity<PostcodePerimeter>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<PostcodePerimeter>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion

            #region Region

            modelBuilder.Entity<Region>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<Region>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion

            #region Roads

            modelBuilder.Entity<Road>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<Road>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion
        }
    }
}
