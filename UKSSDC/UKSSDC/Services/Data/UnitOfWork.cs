using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using UKSSDC.Migrations;
using UKSSDC.Models;

namespace UKSSDC.Services.Data
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Place> Places { get; set; }

        public DbSet<Postcode> Postcodes { get; set; } 

        public DbSet<PostcodePerimeter> PostcodePerimeters { get; set; } 

        public DbSet<Region> Regions { get; set; }

        public DbSet<Road> Roads { get; set; }

        public DbSet<ImportProgress> ImportProgress { get; set; }

        public DbSet<SpatialSearchObject> SpatialSearchObjects { get; set; } 

        public UnitOfWork() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnitOfWork, Configuration>());
            Database.Initialize(false);
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

        public override Task<int> SaveChangesAsync()
        {
            DateTime now = DateTime.UtcNow;
            foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (entry.IsRelationship) continue;
                Common updated = entry.Entity as Common;
                if (updated != null)
                    updated.Updated = now;
            }

            foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                if (entry.IsRelationship) continue;
                Common created = entry.Entity as Common;
                if (created != null)
                    created.Created = now;
            }
            //todo: TEST
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region SpatialObjects

            modelBuilder.Entity<SpatialSearchObject>()
                .Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime2");

            modelBuilder.Entity<SpatialSearchObject>()
                .Property(p => p.Updated)
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion

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
