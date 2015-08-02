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
        public void ReadTest()
        {
            //AAA
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
