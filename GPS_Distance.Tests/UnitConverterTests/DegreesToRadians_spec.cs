using FluentAssertions;
using DistanceCalculator.Helpers;

using Xunit;


namespace UnitConverterTests.DegreesToRadians_spec
{
    public class Given_boundary_values
    {
        private const double expectedPrecision = double.Epsilon;

        [Theory]
        [InlineData(-1, -0.017453292519943295)]
        [InlineData(0, 0)]
        [InlineData(1, 0.017453292519943295)]
        [InlineData(180, 3.141592653589793)] // PI
        [InlineData(359, 6.265732014659643)]
        [InlineData(360, 6.283185307179586)]
        [InlineData(361, 6.300638599699529)]
        public void Should_return_correctly_converted_radians(double degree, double expectedRadian)
        {
            // Act
            var actualRadian = Helper.DegreesToRadians(degree);

            // Assert
            actualRadian.Should().BeApproximately(expectedRadian, expectedPrecision);
        }
    }
}
