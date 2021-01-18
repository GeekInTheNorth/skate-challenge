using System;
using System.Collections.Generic;
using System.Linq;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Routing;
using AllInSkateChallenge.Features.Statistics;

using Moq;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Statistics
{
    [TestFixture]
    public class SkaterTargetAnalyserTests
    {
        private SkaterTargetAnalyser analyser;
        private CheckPointRepository checkPointRepository;
        private Mock<IAbsoluteUrlHelper> mockUrlHelper;

        [SetUp]
        public void SetUp()
        {
            mockUrlHelper = new Mock<IAbsoluteUrlHelper>();
            mockUrlHelper.Setup(x => x.Get(It.IsAny<string>())).Returns("http://mysite.com/image.jpg");
            checkPointRepository = new CheckPointRepository(mockUrlHelper.Object);
            analyser = new SkaterTargetAnalyser(checkPointRepository);
        }

        [Test]
        [TestCase(12.99, false)]
        [TestCase(13, true)]
        public void AnalyseLeavesDateReachedSaltaireEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = distanceInMiles }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.Saltaire)), Is.EqualTo(datePopulated));
        }

        [Test]
        [TestCase(46.99, false)]
        [TestCase(47, true)]
        public void AnalyseLeavesDateReachedFoulridgeEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = distanceInMiles }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.FoulridgeSummit)), Is.EqualTo(datePopulated));
        }

        [Test]
        [TestCase(127.49, false)]
        [TestCase(127.5, true)]
        public void AnalyseLeavesDateReachedLiverpoolEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = distanceInMiles }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.LiverpoolCanningDock)), Is.EqualTo(datePopulated));
        }

        [Test]
        [TestCase(254.99, false)]
        [TestCase(255, true)]
        public void AnalyseLeavesDateReachedLeedsEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = distanceInMiles }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.ThereAndBackAgain)), Is.EqualTo(datePopulated));
        }

        [Test]
        public void AnalyseOnlyCountsLogEntriesBelongingToTheSkater()
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var otherSkaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = 13 },
                new SkateLogEntry { ApplicationUserId = otherSkaterId, DistanceInMiles = 13 }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.TotalSessions, Is.EqualTo(1));
            Assert.That(skaterStats.TotalMiles, Is.EqualTo(13));
        }

        [Test]
        public void AnalyseChecksAllTargetsExceptNone()
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var otherSkaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = 300 }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.None), Is.False);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.AireValleyMarina), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.Saltaire), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.BingleyFiveRiseLocks), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.SkiptonCastle), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.EastMartonDoubleArchedBridge), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.FoulridgeSummit), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.Burnley), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.HalfwayThere), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.BlackburnFlight), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.WiganPier), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.TheScotchPiperInn), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.LiverpoolCanningDock), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.ThereAndBackAgain), Is.True);
        }

        [Test]
        public void AnalyseOnlyCollatesADateForEachCheckPointOnce()
        {
            // Arrange
            var skaterId = Guid.NewGuid().ToString();
            var otherSkaterId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = skaterId };
            var miles = new List<SkateLogEntry>
            {
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = 300 },
                new SkateLogEntry { ApplicationUserId = skaterId, DistanceInMiles = 300 }
            };

            // Act
            var skaterStats = analyser.Analyse(user, miles);

            // Assert
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.None)), Is.EqualTo(0));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.AireValleyMarina)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.Saltaire)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.BingleyFiveRiseLocks)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.SkiptonCastle)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.EastMartonDoubleArchedBridge)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.FoulridgeSummit)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.Burnley)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.HalfwayThere)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.BlackburnFlight)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.WiganPier)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.TheScotchPiperInn)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.LiverpoolCanningDock)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.ThereAndBackAgain)), Is.EqualTo(1));
        }
    }
}
