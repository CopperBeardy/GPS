using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using DistanceCalculator.Helpers;

namespace ParseTests.ParseCoordinates_spec
{
    public class Given_boundary_values
    {
        private const double expectedPrecision = 1e-7;
        private const string precisionFormat = "0.0######";

        [Theory]
        [InlineData(-100.1, 100.1, 0.1)]
        public void LatitudeInputTest(double latitudeStart, double latitudeEnd, double step)
        {
            for (var latitudeToTest = latitudeStart; latitudeToTest <= latitudeEnd; latitudeToTest += step)
            {
                var input = latitudeToTest.ToString(precisionFormat);
                var result = Helper.TryParseLatitude(input, out var latitude);

                if (latitudeToTest >= -90.0000001 && latitudeToTest <= 90.0000001) // Avoid rounding issues.
                {
                    result.Should().BeTrue();
                    latitude.Should().BeGreaterThan(latitudeToTest - expectedPrecision).And.BeLessThan(latitudeToTest + expectedPrecision);
                }
                else
                    result.Should().BeFalse();
            }
        }

        [Theory]
        [InlineData(-190.1, 190.1, 0.1)]
        public void LongitudeInputTest(double longitudeStart, double longitudeEnd, double step)
        {
            for (var longitudeToTest = longitudeStart; longitudeToTest <= longitudeEnd; longitudeToTest += step)
            {
                var input = longitudeToTest.ToString(precisionFormat);
                var result = Helper.TryParseLongitude(input, out var longitude);

                if (longitudeToTest >= -180.0000001 && longitudeToTest <= 180.0000001) // Avoid Rounding issues.
                {
                    result.Should().BeTrue();
                    longitude.Should().BeGreaterThan(longitudeToTest - expectedPrecision).And.BeLessThan(longitudeToTest + expectedPrecision);
                }
                else
                    result.Should().BeFalse();
            }
        }
    }
}
