using Microsoft.VisualStudio.TestTools.UnitTesting;
using UKSSDC.Services.Data;

namespace UKSSDC.Tests.Services.Data
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void SaveASyncTest()
        {
            UnitOfWork uow = new UnitOfWork();

            ImportProgress test = ImportProgress.Create("test async save", RecordType.Place);

            uow.ImportProgress.Add(test);

            uow.SaveASync();
        }
        [TestMethod]
        public void SaveTest()
        {
            UnitOfWork uow = new UnitOfWork();
            
            ImportProgress test = ImportProgress.Create("test vanilla save", RecordType.Place);

            uow.ImportProgress.Add(test);

            uow.Save();
        }
    }
}
