using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Place : Common
    {
        public string Name { get; set; }

        public DbGeography Location { get; set; } //WKT

        public long OsmId { get; set; }

        public Country Country { get; set; }
    }
}
