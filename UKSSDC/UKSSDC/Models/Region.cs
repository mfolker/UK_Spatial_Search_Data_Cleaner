using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Region : Common
    {
        public string name { get; private set; }

        public DbGeography perimeter { get; private set; } //WKT

        public RegionType type { get; private set; }
    }
}
