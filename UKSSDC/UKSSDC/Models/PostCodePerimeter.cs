using System.Data.Entity.Spatial;

namespace UKSSDC.Models
{
    public class PostcodePerimeter : Common
    {
        public string OutwardCode { get; private set; }

        public DbGeography Perimeter { get; private set; } //WKT


    }
}
