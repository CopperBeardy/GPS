using DistanceCalculator.Helpers;
using DistanceCalculator.Models;
using FluentAssertions;

using Xunit;

namespace UnitConverterTests.MetresToMiles_spec
{
    public class Given_boundary_values
    {
        private const double expectedPrecision = 1e-12;

        [Theory]
        [InlineData(-1, -0.000621371192237)]
        [InlineData(0, 0)]
        [InlineData(1, 0.0006213711922373339696174341844)]
        [InlineData(2, 0.00124274238447)]
        [InlineData(1000, 0.621371192237)]
        public void Should_return_correctly_converted_miles(double meters, double expectedMiles)
        {
            // Act
            var actualMiles = Helper.ConvertUnit(Unit.Miles, meters);

            // Assert
            actualMiles.Should().BeApproximately(expectedMiles, expectedPrecision);
        }
    }
}
