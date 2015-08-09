using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Road : Common
    {
        public string Name { get; set; }

        public DbGeography Path { get; set; } //WKT

        public long OsmId { get; set; }

        public string ReferenceNumber { get; set; }

        public string Type { get; set; } //Not implemented as ENUM owing to limited information about exact road types from data provider

        public int MaxSpeed { get; set; }

        public Country Country { get; set; }
    }
}
