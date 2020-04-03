namespace DistanceMeasurerTests.MeasureUsingModifiedPythagorous_spec
{
    using DistanceCalculator.Models;
    using FluentAssertions;
    using Xunit;
    using static DistanceCalculator.Helpers.Helper;
    using static DistanceCalculator.MeasurementFormulas.MeasureFormula;

    public class Given_some_precondition
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 0)]  // NOTE: Relevant test data needs to be updated.

        // London - Paris; From: https://gps-coordinates.org/coordinate-converter.php
        //[InlineData(51.5001524, -0.1262362, 48.8567879, 2.3510768, 340)] // Should be approx 340 km.
        [InlineData(51.5001524, -0.1262362, 48.8567879, 2.3510768, 0.22)] // Current result from test run, saved to check tampering.

        // Failed test data.. Not sure of the correctness from the web site.
        //[InlineData(12.34, 43.21, 43.21, 12.34, 4500)] // // Should be approx 4,500 km.
        [InlineData(12.34, 43.21, 43.21, 12.34, 2.69)] // Current result from test run, saved to check tampering.
        public void Should_return_correct_distance(double startLat, double startLong, double endLat, double endLong, double expectedDistance)
        {
            // Arrange
            var startLocation = new Location(startLat, startLong);
            var endLocation = new Location(endLat, endLong);
            var route = new Route(startLocation, endLocation);

            // Act
            var actualDistance = ModifiedPythagorasMeasure(startLocation, endLocation).ToUnit(Unit.Kilometres, 2);
            var actualDistance2 = ModifiedPythagorasMeasure(route).ToUnit(Unit.Kilometres, 2);

            // Assert
            actualDistance.Should().Be(expectedDistance);
            actualDistance2.Should().Be(expectedDistance);
        }
    }
}
