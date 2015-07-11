using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Import;

namespace UKSSDC.Tests.Services.Import
{
    [TestClass]
    public class ProgressReporterTests
    {

        [TestMethod]
        public void InitialiseTest()
        {
            //TODO: Arrange
            
            //Based on the data available at development time (see readme) the number of files in each directory should be.
            int places = 3;
            int postcodes = 120;
            int regions = 4;
            int roads = 3; 

            //TODO: Act

            IProgressReporter reporter = new ProgressReporter();

            bool result = reporter.Initialise(null); 

            //TODO: Assert

            Assert.IsTrue(result);

        }


        //This test will be turned on and off, as the function will ultimately be private. The aim is to test this in isolation.
        [TestMethod]
        public void AddRecordTest()
        {
            //TODO: Arrange

            string TestPath = "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV\\Places"; 
            
            //TODO: Act
            
            ProgressReporter reporter = new ProgressReporter();

            reporter.AddRecord(TestPath, RecordType.Place);

            //TODO: Assert

            Assert.IsTrue(true);

        }
    }
}
