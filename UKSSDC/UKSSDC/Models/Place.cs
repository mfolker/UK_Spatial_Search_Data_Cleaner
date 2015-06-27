using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKSSDC.Models
{
    public class Place : Common
    {
        public int id { get; private set; }

        public string name { get; set; }

        public dbGeography Location { get; set; }

        public int osm_id { get; set; }

        public Country country { get; private set; }
    }
}
