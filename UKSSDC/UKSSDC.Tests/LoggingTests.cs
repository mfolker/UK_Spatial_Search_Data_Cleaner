using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net; 

namespace UKSSDC.Tests
{
    [TestClass]
    public class LoggingTests
    {

        private static readonly ILog Logger = LogManager.GetLogger("Test"); 

        [TestMethod]
        public void TestMethod1()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Debug("Unit Test Message");    
            //TODO: Add assert.
        }
    }
}
