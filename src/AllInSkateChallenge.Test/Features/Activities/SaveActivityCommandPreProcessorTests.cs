using System.Collections.Generic;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Activities
{
    [TestFixture]
    public class SaveActivityCommandPreProcessorTests
    {
        private Mock<UserManager<ApplicationUser>> mockUserManager;

        private Mock<ApplicationDbContext> mockApplicationDbContext;

        private Mock<ICheckPointRepository> mockCheckPointRepository;

        private Mock<ILogger<SaveActivityCommandPreProcessor>> mockLogger;

        private SaveActivityCommandPreProcessor preProcessor;

        [SetUp]
        public void SetUp()
        {
            mockUserManager = MockUserManager(new List<ApplicationUser>());
            mockApplicationDbContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            mockCheckPointRepository = new Mock<ICheckPointRepository>();
            mockLogger = new Mock<ILogger<SaveActivityCommandPreProcessor>>();

            preProcessor = new SaveActivityCommandPreProcessor(
                mockUserManager.Object,
                mockApplicationDbContext.Object,
                mockCheckPointRepository.Object,
                mockLogger.Object);
        }

        [Test]
        [TestCase(SkateTarget.CornExchange, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.SoveriegnSquare, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.GranaryWharf, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.TetleyBreweryWharf, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.LeedsIndustrialMuseum, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.ArmleyPark, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.EllandRoad, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.MiddletonRailway, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.Carlton, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.TempleNewsamPark, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.LsTen, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.RoyalArmouriesMuseum, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.KirkgateMarket, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.LeedsGrandTheatre, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.MilleniumSquare, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.RamgarhiaSikhSportsCentre, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.PotternewtonPark, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.MeanwoodValleyUrbanFarm, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.YorkshireCricketGround, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.KirkstallAbbey, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.SunnyBankMillsGallery, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.BrownleeCentre, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.GoldenAcrePark, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.EccupReservoir, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.EmmerdaleTheTour, SkateTarget.LeedsBradfordAirport)]
        [TestCase(SkateTarget.HarewoodHouseTrust, SkateTarget.LeedsBradfordAirport)]
        [TestCase(SkateTarget.OtleyChevinForestPark, SkateTarget.LeedsBradfordAirport)]
        [TestCase(SkateTarget.YeadonTarn, SkateTarget.LeedsBradfordAirport)]
        [TestCase(SkateTarget.LeedsBradfordAirport, SkateTarget.LeedsBradfordAirport)]
        public void GetNewTargetReturnsTheCorrectValue(SkateTarget oldTarget, SkateTarget expectedTarget)
        {
            // Act
            var actualTarget = preProcessor.GetNewTarget(oldTarget);

            // Assert
            Assert.That(actualTarget, Is.EqualTo(expectedTarget));
        }

        public Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}