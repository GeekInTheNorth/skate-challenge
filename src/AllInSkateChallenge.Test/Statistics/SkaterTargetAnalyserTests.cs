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
        [TestCase(18.20, false)]
        [TestCase(18.21, true)]
        public void AnalyseLeavesDateReachedTempleNewsamParkEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
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
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.TempleNewsamPark)), Is.EqualTo(datePopulated));
        }

        [Test]
        [TestCase(26.22, false)]
        [TestCase(26.23, true)]
        public void AnalyseLeavesDateReachedPotternewtonParkEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
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
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.PotternewtonPark)), Is.EqualTo(datePopulated));
        }

        [Test]
        [TestCase(47.9, false)]
        [TestCase(47.91, true)]
        public void AnalyseLeavesDateReachedEmmerdaleTheTourEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
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
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.EmmerdaleTheTour)), Is.EqualTo(datePopulated));
        }

        [Test]
        [TestCase(62.13, false)]
        [TestCase(62.14, true)]
        public void AnalyseLeavesDateReachedLeedsBradfordAirportEmptyWhenMilesAreBelowTheThreshold(decimal distanceInMiles, bool datePopulated)
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
            Assert.That(skaterStats.CheckPointDates.Any(x => x.Key.Equals(SkateTarget.LeedsBradfordAirport)), Is.EqualTo(datePopulated));
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
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.CornExchange), Is.False);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.SoveriegnSquare), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.GranaryWharf), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.TetleyBreweryWharf), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.LeedsIndustrialMuseum), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.ArmleyPark), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.EllandRoad), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.MiddletonRailway), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.Carlton), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.TempleNewsamPark), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.LsTen), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.RoyalArmouriesMuseum), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.KirkgateMarket), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.LeedsGrandTheatre), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.MilleniumSquare), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.RamgarhiaSikhSportsCentre), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.PotternewtonPark), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.MeanwoodValleyUrbanFarm), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.YorkshireCricketGround), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.KirkstallAbbey), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.SunnyBankMillsGallery), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.BrownleeCentre), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.GoldenAcrePark), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.EccupReservoir), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.EmmerdaleTheTour), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.HarewoodHouseTrust), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.OtleyChevinForestPark), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.YeadonTarn), Is.True);
            Assert.That(skaterStats.CheckPointDates.ContainsKey(SkateTarget.LeedsBradfordAirport), Is.True);

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
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.CornExchange)), Is.EqualTo(0));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.SoveriegnSquare)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.GranaryWharf)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.TetleyBreweryWharf)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.LeedsIndustrialMuseum)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.ArmleyPark)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.EllandRoad)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.MiddletonRailway)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.Carlton)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.TempleNewsamPark)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.LsTen)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.RoyalArmouriesMuseum)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.KirkgateMarket)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.LeedsGrandTheatre)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.MilleniumSquare)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.RamgarhiaSikhSportsCentre)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.PotternewtonPark)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.MeanwoodValleyUrbanFarm)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.YorkshireCricketGround)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.KirkstallAbbey)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.SunnyBankMillsGallery)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.BrownleeCentre)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.GoldenAcrePark)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.EccupReservoir)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.EmmerdaleTheTour)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.HarewoodHouseTrust)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.OtleyChevinForestPark)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.YeadonTarn)), Is.EqualTo(1));
            Assert.That(skaterStats.CheckPointDates.Count(x => x.Key.Equals(SkateTarget.LeedsBradfordAirport)), Is.EqualTo(1));
        }
    }
}