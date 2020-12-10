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
        [TestCase(SkateTarget.None, SkateTarget.Saltaire)]
        [TestCase(SkateTarget.AireValleyMarina, SkateTarget.Saltaire)]
        [TestCase(SkateTarget.Saltaire, SkateTarget.FoulridgeSummit)]
        [TestCase(SkateTarget.BingleyFiveRiseLocks, SkateTarget.FoulridgeSummit)]
        [TestCase(SkateTarget.SkiptonCastle, SkateTarget.FoulridgeSummit)]
        [TestCase(SkateTarget.EastMartonDoubleArchedBridge, SkateTarget.FoulridgeSummit)]
        [TestCase(SkateTarget.FoulridgeSummit, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.Burnley, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.HalfwayThere, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.BlackburnFlight, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.WiganPier, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.TheScotchPiperInn, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.LiverpoolCanningDock, SkateTarget.ThereAndBackAgain)]
        [TestCase(SkateTarget.ThereAndBackAgain, SkateTarget.ThereAndBackAgain)]
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