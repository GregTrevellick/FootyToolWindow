using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootieData.Common.Tests
{
    [TestClass]
    public class DataGridHelperTests
    {
        [TestMethod]
        public void UpdatedWithinLastXSecondsTest()
        {
            var lastUpdatedDate = new DateTime(2001, 02, 03, 4, 5, 6);
            var now = new DateTime(2001, 02, 03, 4, 5, 8);

            Assert.AreEqual(false, DataGridHelper.UpdatedWithinLastXSeconds(lastUpdatedDate, 1, now));
            Assert.AreEqual(false, DataGridHelper.UpdatedWithinLastXSeconds(lastUpdatedDate, 2, now));
            Assert.AreEqual(true, DataGridHelper.UpdatedWithinLastXSeconds(lastUpdatedDate, 3, now));
        }
    }
}
