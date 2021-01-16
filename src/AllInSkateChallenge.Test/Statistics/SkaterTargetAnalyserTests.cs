using System;
using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Statistics;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Statistics
{
    [TestFixture]
    public class SkaterTargetAnalyserTests
    {
        private SkaterTargetAnalyser analyser;

        [SetUp]
        public void SetUp()
        {
            analyser = new SkaterTargetAnalyser();
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
            Assert.That(skaterStats.DateReachedSaltaire.HasValue, Is.EqualTo(datePopulated));
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
            Assert.That(skaterStats.DateReachedFoulridge.HasValue, Is.EqualTo(datePopulated));
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
            Assert.That(skaterStats.DateReachedLiverpool.HasValue, Is.EqualTo(datePopulated));
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
            Assert.That(skaterStats.DateReachedLeeds.HasValue, Is.EqualTo(datePopulated));
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
    }
}
