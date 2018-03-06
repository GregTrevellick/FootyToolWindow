using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootieData.Gateway.Tests
{
    [TestClass]
    public class MapperHelperTests
    {
        [TestMethod]
        public void MapExternalTeamNameToInternalTeamNameTest()
        {
            Assert.AreEqual(null, MapperHelper.MapExternalTeamNameToInternalTeamName(null));
            Assert.AreEqual(string.Empty, MapperHelper.MapExternalTeamNameToInternalTeamName(""));
            Assert.AreEqual("Manchester United", MapperHelper.MapExternalTeamNameToInternalTeamName("ManU"));
            Assert.AreEqual("Arsenal", MapperHelper.MapExternalTeamNameToInternalTeamName("Arsenal"));
        }
    }
}
