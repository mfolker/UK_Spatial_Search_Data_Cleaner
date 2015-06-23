using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Spatial;


namespace UKSSDC.Models
{
    class PlacesEngland
    {
        public int id { get; private set; }

        public string Name { get; set; }

        public DbGeography Location { get; set; }
    }
}
