using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Road : Common
    {
        public string Name { get; private set; }

        public DbGeography Path { get; private set; } //WKT

        public int OsmId { get; private set; }

        public string ReferenceNumber { get; private set; }

        public string Type { get; private set; } //Not implemented as ENUM owing to limited information about exact road types from data provider

        public int MaxSpeed { get; private set; }

        public Country Country { get; private set; }
    }
}
