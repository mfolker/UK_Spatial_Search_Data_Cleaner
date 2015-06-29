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
        public string postcode { get; private set; }

        public string positionalQualityIndicator { get; private set; }

        public int eastings { get; private set; }

        public int northings { get; private set; }

        public DbGeography location { get; private set; } //WKT
    }
}
