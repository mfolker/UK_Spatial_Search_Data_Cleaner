using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC.Tests.Services.Import
{
    [TestClass]
    public class CsvReaderTests
    {
        [TestMethod]
        public void ReadPartTest()
        {
            string filePath =
                "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV\\Places\\PlacesWales.csv";

            IUnitOfWork uow = new UnitOfWork();

            CsvReader reader = new CsvReader(uow);

            var result = reader.Read(filePath, 1, true);

            //TODO: Add assert. 
        }

        public void ReadTest()
        {
            //TODO: Write a test for reading the rest of the file. 
        }

        [TestMethod]
        public void TotalRecordsTest()
        {
            string filePath = 
                "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV\\Places\\PlacesWales.csv"; 

            IUnitOfWork uow = new UnitOfWork(); 

            CsvReader reader = new CsvReader(uow);

            reader.TotalRecords(filePath, RecordType.Place).ShouldBe(2759);

        }

    }
}
