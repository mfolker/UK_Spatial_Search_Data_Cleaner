using System;
using System.Data.Entity.Spatial;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC.Tests
{
    [TestClass]
    public class PostcodesTests
    {
        [TestMethod]
        public void PostcodeWktTest()
        {
            //Arrange

            IUnitOfWork uowPR = new UnitOfWork();
            ICsvReader csvReaderPR = new CsvReader(uowPR);
            IProgressReporter progressReporter = new ProgressReporter(uowPR, csvReaderPR);

            IUnitOfWork uowPostcodes = new UnitOfWork();
            ICsvReader csvReaderPostcodes = new CsvReader(uowPR);

            Postcodes postcodes = new Postcodes(csvReaderPostcodes, progressReporter, uowPostcodes);
            PrivateObject postcodesPrivateObject = new PrivateObject(postcodes);

            //Easting Northings for AB101YN
            int easting = 392564; // X Co-ordinate, Longitiude
            int northing = 805767; // Y Co-ordinate, Latitude

            object[] param = {easting, northing}; 

            //Act

            DbGeography ab101Yn = (DbGeography) postcodesPrivateObject.Invoke("PostcodeWkt", param); 

            //Assert 

            //"POINT (-2.124517 57.142721)"

            Assert.IsTrue(ab101Yn.Longitude == -2.1245 && ab101Yn.Latitude == 57.1427);
        }
    }
}


/*
 http://www.bgs.ac.uk/data/webservices/convertForm.cfm
*/