﻿using System.Data.Entity.Spatial;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class Region : Common
    {
        public string Name { get; private set; }

        public DbGeography Perimeter { get; private set; } //WKT

        public RegionType Type { get; private set; }
    }
}
