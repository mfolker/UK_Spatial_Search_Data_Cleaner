using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Place : Common
    {
        public string name { get; set; }

        public DbGeography location { get; set; } //WKT

        public int osm_id { get; set; }

        public Country country { get; private set; }
    }
}
