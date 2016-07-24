using System.Data.Entity.Spatial;

namespace UKSSDC.Models
{
    public class PostcodePerimeter : Common
    {
        //Outward code can be derived from postcode records
        public string OutwardCode { get; set; }

        public DbGeography Perimeter { get; set; } //WKT


    }
}
