using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Place : Common
    {
        public string Name { get; set; }

        public DbGeography Location { get; set; } //WKT

        public int OsmId { get; set; }

        public Country Country { get; private set; }
    }
}
