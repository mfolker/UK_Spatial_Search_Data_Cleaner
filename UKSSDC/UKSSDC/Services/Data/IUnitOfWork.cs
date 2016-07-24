using System.Data.Entity;
using System.Threading.Tasks;
using UKSSDC.Models;
using UKSSDC.Services.Autofac;

namespace UKSSDC.Services.Data
{
    public interface IUnitOfWork : IDependency
    {
        DbSet<Place> Places { get; set; }
        DbSet<Postcode> Postcodes { get; set; }
        DbSet<PostcodePerimeter> PostcodePerimeters { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<Road> Roads { get; set; }
        DbSet<ImportProgress> ImportProgress { get; set; }
        DbSet<SpatialSearchObject> SpatialSearchObjects { get; set; } 

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}