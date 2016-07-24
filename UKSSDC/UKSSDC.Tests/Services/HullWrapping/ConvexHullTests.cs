using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UKSSDC.Tests.Services.HullWrapping
{
    [TestClass]
    public class ConvexHullTests
    {
        [TestMethod]
        public void BuildHullTest()
        {
            List<DbGeography> input = new List<DbGeography>
            {
                DbGeography.PointFromText("", 4326),
                DbGeography.PointFromText("", 4326), 
            };

        }
    }
}
