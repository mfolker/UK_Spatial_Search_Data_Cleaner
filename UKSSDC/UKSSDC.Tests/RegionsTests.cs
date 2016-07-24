using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC.Tests
{
    [TestClass]
    public class RegionsTests
    {
        [TestMethod]
        public void SplitCsvLineRegionTest()
        {
            //Arrange
            IUnitOfWork uowPR = new UnitOfWork();
            ICsvReader csvReaderPR = new CsvReader(uowPR);
            IProgressReporter progressReporter = new ProgressReporter(uowPR, csvReaderPR);

            IUnitOfWork uowRegions = new UnitOfWork();
            ICsvReader csvReaderRegions = new CsvReader(uowPR);
            Regions regions = new Regions(csvReaderRegions, progressReporter, uowRegions);
            PrivateObject regionsPrivateObject = new PrivateObject(regions);

            string filePath = "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV\\Regions\\CountyRegion.csv";

            var csvLines = File.ReadAllLines(filePath).ToList();

            object[] param = { csvLines[1] };

            //Act
            List<string> result = (List<string>) regionsPrivateObject.Invoke("SplitCsvLineRegion", param); 

            //Assert
            result[1].ShouldBe("Buckinghamshire County"); //First lines of csv file too long to add to a unit test.

        }
    }
}
