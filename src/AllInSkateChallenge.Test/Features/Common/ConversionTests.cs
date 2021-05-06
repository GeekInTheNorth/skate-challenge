using System;

using AllInSkateChallenge.Features.Common;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Common
{
    [TestFixture]
    public class ConversionTests
    {
        [Test]
        [TestCase(1, 0.3048)]
        [TestCase(3, 0.9144)]
        [TestCase(99, 30.1752)]
        public void FeetToMetres_CorrectlyConvertsValues(decimal feet, decimal expectedMetres)
        {
            Assert.That(Math.Round(Conversion.FeetToMetres(feet), 6), Is.EqualTo(expectedMetres));
        }

        [Test]
        [TestCase(1000, 0.189394)]
        [TestCase(2000, 0.378788)]
        [TestCase(5280, 1)]
        public void FeetToMiles_CorrectlyConvertsValues(decimal feet, decimal expectedMiles)
        {
            Assert.That(Math.Round(Conversion.FeetToMiles(feet), 6), Is.EqualTo(expectedMiles));
        }

        [Test]
        [TestCase(3280.84, 1)]
        [TestCase(1000, 0.3048)]
        [TestCase(5280, 1.609344)]
        public void FeetToKilometres_CorrectlyConvertsValues(decimal feet, decimal expectedKilometres)
        {
            Assert.That(Math.Round(Conversion.FeetToKilometres(feet), 6), Is.EqualTo(expectedKilometres));
        }

        [Test]
        [TestCase(1, 1000)]
        [TestCase(3, 3000)]
        [TestCase(1.678, 1678)]
        public void KilometresToMetres_CorrectlyConvertsValues(decimal kilometres, decimal expectedMetres)
        {
            Assert.That(Math.Round(Conversion.KilometresToMetres(kilometres), 6), Is.EqualTo(expectedMetres));
        }

        [Test]
        [TestCase(1, 0.621371)]
        [TestCase(5, 3.106856)]
        [TestCase(1.609344, 1)]
        public void KilometresToMiles_CorrectlyConvertsValues(decimal kilometres, decimal expectedMiles)
        {
            Assert.That(Math.Round(Conversion.KilometresToMiles(kilometres), 6), Is.EqualTo(expectedMiles));
        }

        [Test]
        [TestCase(1, 3280.839895)]
        [TestCase(5, 16404.199476)]
        [TestCase(1.609344, 5280)]
        public void KilometresToFeet_CorrectlyConvertsValues(decimal kilometres, decimal expectedFeet)
        {
            Assert.That(Math.Round(Conversion.KilometresToFeet(kilometres), 6), Is.EqualTo(expectedFeet));
        }

        [Test]
        [TestCase(1, 0.001)]
        [TestCase(1000, 1)]
        [TestCase(2500, 2.5)]
        public void MetresToKilometres_CorrectlyConvertsValues(decimal metres, decimal expectedKilometres)
        {
            Assert.That(Math.Round(Conversion.MetresToKilometres(metres), 6), Is.EqualTo(expectedKilometres));
        }

        [Test]
        [TestCase(1.609344, 0.001)]
        [TestCase(1609.344, 1)]
        [TestCase(5280, 3.28084)]
        public void MetresToMiles_CorrectlyConvertsValues(decimal metres, decimal expectedMiles)
        {
            Assert.That(Math.Round(Conversion.MetresToMiles(metres), 6), Is.EqualTo(expectedMiles));
        }

        [Test]
        [TestCase(1, 3.28084)]
        [TestCase(304.8, 1000)]
        [TestCase(1000, 3280.839895)]
        public void MetresToFeet_CorrectlyConvertsValues(decimal metres, decimal expectedFeet)
        {
            Assert.That(Math.Round(Conversion.MetresToFeet(metres), 6), Is.EqualTo(expectedFeet));
        }

        [Test]
        [TestCase(1, 1609.344)]
        [TestCase(2, 3218.688)]
        [TestCase(3.5, 5632.704)]
        public void MilesToMetres_CorrectlyConvertsValues(decimal miles, decimal expectedMetres)
        {
            Assert.That(Math.Round(Conversion.MilesToMetres(miles), 6), Is.EqualTo(expectedMetres));
        }

        [Test]
        [TestCase(1, 1.609344)]
        [TestCase(2, 3.218688)]
        [TestCase(3.5, 5.632704)]
        public void MilesToKilometres_CorrectlyConvertsValues(decimal miles, decimal expectedKilometres)
        {
            Assert.That(Math.Round(Conversion.MilesToKilometres(miles), 6), Is.EqualTo(expectedKilometres));
        }

        [Test]
        [TestCase(1, 5280)]
        [TestCase(2, 10560)]
        [TestCase(3.5, 18480)]
        public void MilesToFeet_CorrectlyConvertsValues(decimal miles, decimal expectedFeet)
        {
            Assert.That(Math.Round(Conversion.MilesToFeet(miles), 6), Is.EqualTo(expectedFeet));
        }

        [Test]
        [TestCase(1, 2.236936)]
        [TestCase(2, 4.473873)]
        [TestCase(3.5, 7.829277)]
        public void MetresPerSecondToMilesPerHour_CorrectlyConvertsValues(decimal metresPerSecond, decimal expectedMilesPerHour)
        {
            Assert.That(Math.Round(Conversion.MetresPerSecondToMilesPerHour(metresPerSecond), 6), Is.EqualTo(expectedMilesPerHour));
        }

        [Test]
        [TestCase(1, 0.621371)]
        [TestCase(1.609344, 1)]
        [TestCase(2, 1.242742)]
        public void KilometresPerHourToMilesPerHour_CorrectlyConvertsValues(decimal kilometresPerHour, decimal expectedMilesPerHour)
        {
            Assert.That(Math.Round(Conversion.KilometresPerHourToMilesPerHour(kilometresPerHour), 6), Is.EqualTo(expectedMilesPerHour));
        }

        [Test]
        [TestCase(1, 1.609344)]
        [TestCase(0.621371, 1)]
        [TestCase(2, 3.218688)]
        public void MilesPerHourToKilometresPerHour_CorrectlyConvertsValues(decimal kilometresPerHour, decimal expectedMilesPerHour)
        {
            Assert.That(Math.Round(Conversion.MilesPerHourToKilometresPerHour(kilometresPerHour), 6), Is.EqualTo(expectedMilesPerHour));
        }
    }
}
