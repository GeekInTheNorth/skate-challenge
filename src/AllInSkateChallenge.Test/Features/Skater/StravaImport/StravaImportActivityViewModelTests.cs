using System;

using AllInSkateChallenge.Features.Skater.StravaImport;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Skater.StravaImport
{
    [TestFixture]
    public sealed class StravaImportActivityViewModelTests
    {
        [Test]
        [TestCase("IceSkate", "2023-05-31T23:59:59", false)]
        [TestCase("InlineSkate", "2023-05-31T23:59:59", false)]
        [TestCase("Skateboard", "2023-05-31T23:59:59", false)]
        [TestCase("InvalidType", "2023-05-31T23:59:59", false)]
        [TestCase("IceSkate", "2023-06-01T00:00:00", true)]
        [TestCase("InlineSkate", "2023-06-01T00:00:00", true)]
        [TestCase("Skateboard", "2023-06-01T00:00:00", true)]
        [TestCase("InvalidType", "2023-06-01T00:00:00", false)]
        public void EligableActivities_OnlyReturnsTrueForSkateEventsAfterTheEventStart(
            string activityType,
            DateTime startDate,
            bool expectedValue)
        {
            // Arrange
            var activity = new StravaImportActivityViewModel
            {
                ActivityType = activityType,
                StartDate = startDate,
            };

            // Assert
            Assert.That(activity.IsEligableActivity, Is.EqualTo(expectedValue));
        }
    }
}
