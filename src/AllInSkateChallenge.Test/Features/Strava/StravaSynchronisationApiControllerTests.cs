using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava;
using AllInSkateChallenge.Features.Strava.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;

using Moq;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Strava
{
    [TestFixture]
    public class StravaSynchronisationApiControllerTests
    {
        Mock<IMediator> _mockMediator;

        Mock<IUserStore<ApplicationUser>> _mockUserStore;

        Mock<UserManager<ApplicationUser>> _mockUserManager;

        Mock<ApplicationUser> _mockUser;

        private StravaSynchronisationApiController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();

            _mockUser = new Mock<ApplicationUser>();

            _mockUserStore = new Mock<IUserStore<ApplicationUser>>();

            _mockUserManager = new Mock<UserManager<ApplicationUser>>(_mockUserStore.Object, null, null, null, null, null, null, null, null);
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(_mockUser.Object);

            _controller = new StravaSynchronisationApiController(_mockMediator.Object, _mockUserManager.Object);
        }

        [Test]
        [TestCaseSource(
            typeof(StravaSynchronisationApiControllerTestCases), 
            nameof(StravaSynchronisationApiControllerTestCases.ImportLatestSaveOrIgnoreTestCases))]
        public void ImportLatestOnlySavesEventsWithValidStartDatesAndActivityTypes(
            string activityType,
            DateTime activityStartDate,
            int expectedSaves,
            int expectedIgnores)
        {
            // Arrange
            var response = new StravaImportPendingImportsResponse
            {
                Activities = new List<StravaActivity>
                {
                    new StravaActivity
                    {
                        ActivityType = activityType,
                        StartDate = activityStartDate
                    }
                }
            };

            _mockMediator.Setup(x => x.Send(It.IsAny<StravaPendingImportsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            _ = _controller.ImportLatest();

            // Assert
            _mockMediator.Verify(x => x.Send(It.IsAny<SaveActivityCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(expectedSaves));
            _mockMediator.Verify(x => x.Send(It.IsAny<IgnoreActivitiesCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(expectedIgnores));
        }
    }
}