using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            bool result = reporter.Initialise(); 

            //TODO: Assert

            Assert.IsTrue(result == true);

        }
    }
}
