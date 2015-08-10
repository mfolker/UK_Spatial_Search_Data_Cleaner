using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using UKSSDC;

namespace UKSSDC.Tests
{
    [TestClass]
    public class RecordsCommonTests
    {
        [TestMethod]
        public void SplitCsvLinePipeTest()
        {
            RecordsCommon recordHandler = new RecordsCommon();
            PrivateObject hanlder = new PrivateObject(recordHandler);

            object[] param = {"\"LINESTRING (-1.8212114 52.5538901,-1.8205573 52.554324)\"|45,Douglas Road||residential|0|0|0|49"};

            string[] result = (string[])hanlder.Invoke("SplitCsvLinePipe", param);

            result[0].ShouldBe("LINESTRING (-1.8212114 52.5538901,-1.8205573 52.554324)");
        }

        [TestMethod]
        public void SplitCsvLineCommaTest()
        {
            RecordsCommon recordHandler = new RecordsCommon();
            PrivateObject hanlder = new PrivateObject(recordHandler);

            object[] param = { "\"POINT (-0.1276474 51.5073219)\", 107775, London, city, 8416535" };

            string[] result = (string[])hanlder.Invoke("SplitCsvLineComma", param);

            result[0].ShouldBe("POINT (-0.1276474 51.5073219)");
        }
    }
}
