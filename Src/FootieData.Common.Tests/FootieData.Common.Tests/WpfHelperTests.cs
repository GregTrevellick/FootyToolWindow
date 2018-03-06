using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootieData.Common.Tests
{
    [TestClass]
    public class WpfHelperTests
    {
        [TestMethod]
        public void GetPleaseWaitTimeTest()
        {
            var lastUpdatedDate = new DateTime(2001, 02, 03, 4, 5, 4);
            var dateTimeNow = new DateTime(2001, 02, 03, 4, 5, 6);

            Assert.AreEqual(8, WpfHelper.GetPleaseWaitTime(lastUpdatedDate, dateTimeNow, 10));
            Assert.AreEqual(68, WpfHelper.GetPleaseWaitTime(lastUpdatedDate, dateTimeNow, 70));

            dateTimeNow = new DateTime(2001, 02, 03, 4, 7, 6);
            Assert.AreEqual(-52, WpfHelper.GetPleaseWaitTime(lastUpdatedDate, dateTimeNow, 70));
        }
    }
}
