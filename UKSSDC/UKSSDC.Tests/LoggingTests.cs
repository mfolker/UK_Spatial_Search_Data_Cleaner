using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UKSSDC.Tests
{
    [TestClass]
    public class LoggingTests
    {

        private static readonly ILog Logger = LogManager.GetLogger("Test"); 

        [TestMethod]
        public void TestMethod1()
        {
            XmlConfigurator.Configure();
            Logger.Debug("Unit Test Message");    
            //TODO: Add assert.
        }
    }
}
