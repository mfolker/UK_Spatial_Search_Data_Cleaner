using System.Data.Entity.Spatial;

namespace UKSSDC.Models
{
    public class PostcodePerimeter : Common
    {
        //Outward code can be derived from postcode records
        public string OutwardCode { get; private set; }

        public DbGeography Perimeter { get; private set; } //WKT


    }
}
