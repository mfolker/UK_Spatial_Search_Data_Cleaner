using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
