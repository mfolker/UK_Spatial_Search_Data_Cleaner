using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Region : Common
    {
        public string Name { get; set; }

        public DbGeography Perimeter { get; set; } //WKT

        public RegionType Type { get; set; }
    }
}
