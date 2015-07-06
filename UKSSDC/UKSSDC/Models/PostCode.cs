using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;


namespace UKSSDC.Models
{
    public class PostCode : Common
    {
        public string Postcode { get; private set; }

        public string PositionalQualityIndicator { get; private set; }

        public int Eastings { get; private set; }

        public int Northings { get; private set; }

        public DbGeography Location { get; private set; } //WKT
    }
}
