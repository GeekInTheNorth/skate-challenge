using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Extensions;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Extensions
{
    [TestFixture]
    public class EnumExtensionTests
    {
        [Test]
        [TestCase(SkateTarget.None, SkateTarget.AireValleyMarina)]
        [TestCase(SkateTarget.AireValleyMarina, SkateTarget.Saltaire)]
        [TestCase(SkateTarget.Saltaire, SkateTarget.BingleyFiveRiseLocks)]
        [TestCase(SkateTarget.BingleyFiveRiseLocks, SkateTarget.SkiptonCastle)]
        [TestCase(SkateTarget.SkiptonCastle, SkateTarget.EastMartonDoubleArchedBridge)]
        [TestCase(SkateTarget.EastMartonDoubleArchedBridge, SkateTarget.FoulridgeSummit)]
        [TestCase(SkateTarget.FoulridgeSummit, SkateTarget.Burnley)]
        [TestCase(SkateTarget.Burnley, SkateTarget.HalfwayThere)]
        [TestCase(SkateTarget.HalfwayThere, SkateTarget.BlackburnFlight)]
        [TestCase(SkateTarget.BlackburnFlight, SkateTarget.WiganPier)]
        [TestCase(SkateTarget.WiganPier, SkateTarget.TheScotchPiperInn)]
        [TestCase(SkateTarget.TheScotchPiperInn, SkateTarget.LiverpoolCanningDock)]
        [TestCase(SkateTarget.LiverpoolCanningDock, SkateTarget.ThereAndBackAgain)]
        [TestCase(SkateTarget.ThereAndBackAgain, SkateTarget.None)]
        public void NextMovesToNextEnumValue(SkateTarget currentValue, SkateTarget expectedValue)
        {
            // Act
            var actualValue = currentValue.Next();

            // Assert
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase(SkateTarget.None, SkateTarget.ThereAndBackAgain)]
        [TestCase(SkateTarget.AireValleyMarina, SkateTarget.None)]
        [TestCase(SkateTarget.Saltaire, SkateTarget.AireValleyMarina)]
        [TestCase(SkateTarget.BingleyFiveRiseLocks, SkateTarget.Saltaire)]
        [TestCase(SkateTarget.SkiptonCastle, SkateTarget.BingleyFiveRiseLocks)]
        [TestCase(SkateTarget.EastMartonDoubleArchedBridge, SkateTarget.SkiptonCastle)]
        [TestCase(SkateTarget.FoulridgeSummit, SkateTarget.EastMartonDoubleArchedBridge)]
        [TestCase(SkateTarget.Burnley, SkateTarget.FoulridgeSummit)]
        [TestCase(SkateTarget.HalfwayThere, SkateTarget.Burnley)]
        [TestCase(SkateTarget.BlackburnFlight, SkateTarget.HalfwayThere)]
        [TestCase(SkateTarget.WiganPier, SkateTarget.BlackburnFlight)]
        [TestCase(SkateTarget.TheScotchPiperInn, SkateTarget.WiganPier)]
        [TestCase(SkateTarget.LiverpoolCanningDock, SkateTarget.TheScotchPiperInn)]
        [TestCase(SkateTarget.ThereAndBackAgain, SkateTarget.LiverpoolCanningDock)]
        public void PreviousMovesToPreviousEnumValue(SkateTarget currentValue, SkateTarget expectedValue)
        {
            // Act
            var actualValue = currentValue.Previous();

            // Assert
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }
    }
}
