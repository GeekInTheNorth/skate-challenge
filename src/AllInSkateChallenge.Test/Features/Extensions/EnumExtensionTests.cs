using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Extensions;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Extensions
{
    [TestFixture]
    public class EnumExtensionTests
    {
        [Test]
        [TestCase(SkateTarget.CornExchange, SkateTarget.SoveriegnSquare)]
        [TestCase(SkateTarget.SoveriegnSquare, SkateTarget.GranaryWharf)]
        [TestCase(SkateTarget.GranaryWharf, SkateTarget.TetleyBreweryWharf)]
        [TestCase(SkateTarget.TetleyBreweryWharf, SkateTarget.LeedsIndustrialMuseum)]
        [TestCase(SkateTarget.LeedsIndustrialMuseum, SkateTarget.ArmleyPark)]
        [TestCase(SkateTarget.ArmleyPark, SkateTarget.EllandRoad)]
        [TestCase(SkateTarget.EllandRoad, SkateTarget.MiddletonRailway)]
        [TestCase(SkateTarget.MiddletonRailway, SkateTarget.Carlton)]
        [TestCase(SkateTarget.Carlton, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.TempleNewsamPark, SkateTarget.LsTen)]
        [TestCase(SkateTarget.LsTen, SkateTarget.RoyalArmouriesMuseum)]
        [TestCase(SkateTarget.RoyalArmouriesMuseum, SkateTarget.KirkgateMarket)]
        [TestCase(SkateTarget.KirkgateMarket, SkateTarget.LeedsGrandTheatre)]
        [TestCase(SkateTarget.LeedsGrandTheatre, SkateTarget.MilleniumSquare)]
        [TestCase(SkateTarget.MilleniumSquare, SkateTarget.RamgarhiaSikhSportsCentre)]
        [TestCase(SkateTarget.RamgarhiaSikhSportsCentre, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.PotternewtonPark, SkateTarget.MeanwoodValleyUrbanFarm)]
        [TestCase(SkateTarget.MeanwoodValleyUrbanFarm, SkateTarget.YorkshireCricketGround)]
        [TestCase(SkateTarget.YorkshireCricketGround, SkateTarget.KirkstallAbbey)]
        [TestCase(SkateTarget.KirkstallAbbey, SkateTarget.SunnyBankMillsGallery)]
        [TestCase(SkateTarget.SunnyBankMillsGallery, SkateTarget.BrownleeCentre)]
        [TestCase(SkateTarget.BrownleeCentre, SkateTarget.GoldenAcrePark)]
        [TestCase(SkateTarget.GoldenAcrePark, SkateTarget.EccupReservoir)]
        [TestCase(SkateTarget.EccupReservoir, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.EmmerdaleTheTour, SkateTarget.HarewoodHouseTrust)]
        [TestCase(SkateTarget.HarewoodHouseTrust, SkateTarget.OtleyChevinForestPark)]
        [TestCase(SkateTarget.OtleyChevinForestPark, SkateTarget.YeadonTarn)]
        [TestCase(SkateTarget.YeadonTarn, SkateTarget.LeedsBradfordAirport)]
        public void NextMovesToNextEnumValue(SkateTarget currentValue, SkateTarget expectedValue)
        {
            // Act
            var actualValue = currentValue.Next();

            // Assert
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase(SkateTarget.SoveriegnSquare, SkateTarget.CornExchange)]
        [TestCase(SkateTarget.GranaryWharf, SkateTarget.SoveriegnSquare)]
        [TestCase(SkateTarget.TetleyBreweryWharf, SkateTarget.GranaryWharf)]
        [TestCase(SkateTarget.LeedsIndustrialMuseum, SkateTarget.TetleyBreweryWharf)]
        [TestCase(SkateTarget.ArmleyPark, SkateTarget.LeedsIndustrialMuseum)]
        [TestCase(SkateTarget.EllandRoad, SkateTarget.ArmleyPark)]
        [TestCase(SkateTarget.MiddletonRailway, SkateTarget.EllandRoad)]
        [TestCase(SkateTarget.Carlton, SkateTarget.MiddletonRailway)]
        [TestCase(SkateTarget.TempleNewsamPark, SkateTarget.Carlton)]
        [TestCase(SkateTarget.LsTen, SkateTarget.TempleNewsamPark)]
        [TestCase(SkateTarget.RoyalArmouriesMuseum, SkateTarget.LsTen)]
        [TestCase(SkateTarget.KirkgateMarket, SkateTarget.RoyalArmouriesMuseum)]
        [TestCase(SkateTarget.LeedsGrandTheatre, SkateTarget.KirkgateMarket)]
        [TestCase(SkateTarget.MilleniumSquare, SkateTarget.LeedsGrandTheatre)]
        [TestCase(SkateTarget.RamgarhiaSikhSportsCentre, SkateTarget.MilleniumSquare)]
        [TestCase(SkateTarget.PotternewtonPark, SkateTarget.RamgarhiaSikhSportsCentre)]
        [TestCase(SkateTarget.MeanwoodValleyUrbanFarm, SkateTarget.PotternewtonPark)]
        [TestCase(SkateTarget.YorkshireCricketGround, SkateTarget.MeanwoodValleyUrbanFarm)]
        [TestCase(SkateTarget.KirkstallAbbey, SkateTarget.YorkshireCricketGround)]
        [TestCase(SkateTarget.SunnyBankMillsGallery, SkateTarget.KirkstallAbbey)]
        [TestCase(SkateTarget.BrownleeCentre, SkateTarget.SunnyBankMillsGallery)]
        [TestCase(SkateTarget.GoldenAcrePark, SkateTarget.BrownleeCentre)]
        [TestCase(SkateTarget.EccupReservoir, SkateTarget.GoldenAcrePark)]
        [TestCase(SkateTarget.EmmerdaleTheTour, SkateTarget.EccupReservoir)]
        [TestCase(SkateTarget.HarewoodHouseTrust, SkateTarget.EmmerdaleTheTour)]
        [TestCase(SkateTarget.OtleyChevinForestPark, SkateTarget.HarewoodHouseTrust)]
        [TestCase(SkateTarget.YeadonTarn, SkateTarget.OtleyChevinForestPark)]
        [TestCase(SkateTarget.LeedsBradfordAirport, SkateTarget.YeadonTarn)]
        public void PreviousMovesToPreviousEnumValue(SkateTarget currentValue, SkateTarget expectedValue)
        {
            // Act
            var actualValue = currentValue.Previous();

            // Assert
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }
    }
}