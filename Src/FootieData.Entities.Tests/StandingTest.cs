using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootieData.Entities.Tests
{
    [TestClass]
    public class StandingTest
    {
        [TestMethod]
        public void StandingTest1()
        {
            var standing = new Standing
            {
                AwayWins = 1,
                AwayDraws = 2,
                AwayLosses = 3,
                HomeWins = 10,
                HomeDraws = 20,
                HomeLosses = 30,
            };

            Assert.AreEqual(6, standing.AwayPlayed);
            Assert.AreEqual(60, standing.HomePlayed);
            Assert.AreEqual(11, standing.Wins);
            Assert.AreEqual(22, standing.Draws);
            Assert.AreEqual(33, standing.Losses);
            Assert.AreEqual(5, standing.AwayPoints);
            Assert.AreEqual(50, standing.HomePoints);            
        }
    }
}
