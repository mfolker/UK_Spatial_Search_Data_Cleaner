using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC.Tests.Services.Import
{
    [TestClass]
    public class ProgressReporterTests
    {

        [TestMethod]
        public void ReportTest()
        {
            var uow = new UnitOfWork();

            uow.Database.ExecuteSqlCommand("DELETE FROM ImportProgresses");

            InitialiseTest();

            IProgressReporter reporter = new ProgressReporter(uow);

            var reports = reporter.Report(RecordType.Place);

            int count = reports.Count;

            count.ShouldBe(3);
        }

        [TestMethod]
        public void InitialiseTest()
        {
            //Arrange
            
            //Based on the data available at development time (see readme) the number of files in each directory should be.
            int places = 3;
            int postcodes = 120;
            int regions = 4;
            int roads = 3;

            var uow = new UnitOfWork();

            // Act

            IProgressReporter reporter = new ProgressReporter(uow);

            bool result = reporter.Initialise(""); 

            //Assert

            Assert.IsTrue(result);

        }


        //This test will be turned on and off, as the function will ultimately be private. The aim is to test this in isolation.
        [TestMethod]
        public void AddRecordTest()
        {
            //Arrange (Mock Dependencies)
            var uow = new UnitOfWork();
            string TestPath = "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV\\Places";

            uow.Database.ExecuteSqlCommand("DELETE FROM ImportProgresses");

            // Act
            //ProgressReporter reporter = new ProgressReporter(uow);
            ProgressReporter progressReporter = new ProgressReporter(uow);
            PrivateObject reporter = new PrivateObject(progressReporter);

            //PrivateObject reporter = new PrivateObject(typeof(ProgressReporter(uow)));
            object[] param = {TestPath, RecordType.Place};
            
            reporter.Invoke("AddRecord", param);
             
            //reporter.AddRecord(TestPath, RecordType.Place);

            var uow2 = new UnitOfWork();
            var placesRecords = uow2.ImportProgress.Count(x => x.RecordType == RecordType.Place);

            //Assert

            Assert.IsTrue(placesRecords == 3);

        }
    }
}
