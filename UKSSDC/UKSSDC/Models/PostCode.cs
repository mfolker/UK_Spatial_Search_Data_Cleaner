using System.Data.Entity.Spatial;

namespace UKSSDC.Models
{
    public class Postcode : Common
    {
        public string Area { get; set; }

        public string FullPostcode { get; set; }

        public string PositionalQualityIndicator { get; set; }

        public int Easting { get; set; }

        public int Northing { get; set; }

        public DbGeography Location { get; set; } //WKT

        public string OutwardCode { get; set; }
    }
}
