using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

namespace UKSSDC.Models
{
    public class PostCodePerimeter : Common
    {
        public string OutwardCode { get; private set; }

        public DbGeography Perimeter { get; private set; } //WKT


    }
}
