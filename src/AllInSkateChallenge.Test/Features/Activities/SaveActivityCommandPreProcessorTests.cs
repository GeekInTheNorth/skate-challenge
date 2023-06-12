namespace AllInSkateChallenge.Test.Features.Activities;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Static;

using NUnit.Framework;

[TestFixture]
public class SaveActivityCommandPreProcessorTests
{
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
    [TestCase(SkateTarget.LeedsBradfordAirport, SkateTarget.ThereAndBackAgain)]
    [TestCase(SkateTarget.ThereAndBackAgain, SkateTarget.ThereAndBackAgain)]
    public void GetNewTargetReturnsTheCorrectValue(SkateTarget oldTarget, SkateTarget expectedTarget)
    {
        // Act
        var actualTarget = SaveActivityCommandPreProcessor.GetNewTarget(oldTarget);

        // Assert
        Assert.That(actualTarget, Is.EqualTo(expectedTarget));
    }
}