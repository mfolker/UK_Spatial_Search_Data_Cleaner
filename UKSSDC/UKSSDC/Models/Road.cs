using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Road : Common
    {
        public string name { get; private set; }

        public DbGeography path { get; private set; } //WKT

        public int osm_id { get; private set; }

        public string referenceNumber { get; private set; }

        public string type { get; private set; } //Not implemented as ENUM owing to limited information about exact road types from data provider

        public int maxSpeed { get; private set; }
    }
}
