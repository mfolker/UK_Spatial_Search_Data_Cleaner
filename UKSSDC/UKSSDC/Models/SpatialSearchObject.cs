using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class SpatialSearchObject : Common
    {
        public string Name { get; set; }

        public DbGeography SpatialObject { get; set; } //WKT

        public SpatialObjectType Type { get; set; }
    }
}
