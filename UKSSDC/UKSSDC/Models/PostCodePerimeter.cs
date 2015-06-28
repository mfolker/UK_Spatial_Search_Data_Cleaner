using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Spatial;

namespace UKSSDC.Models
{
    public class PostCodePerimeter : Common
    {
        public string outwardCode { get; private set; }

        public DbGeography perimeter { get; private set; } //WKT


    }
}
