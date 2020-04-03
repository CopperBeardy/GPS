using DistanceCalculator.Helpers;
using FluentAssertions;
using Xunit;

namespace UnitConverterTests.RadiansToDegrees_spec
{
    public class Given_boundary_values
    {
        private const double expectedPrecision = 1e-12;

        [Theory]
        [InlineData(-1, -57.2957795130823)]
        [InlineData(1, 57.2957795130823)]
        [InlineData(2, 114.591559026165)]
        [InlineData(3.141592653589793, 180)] // PI
        public void Should_return_correctly_converted_degrees(double radian, double expectedDegrees)
        {
            // Act
            var actualDegrees = Helper.RadiansToDegrees(radian);

            // Assert
            actualDegrees.Should().BeApproximately(expectedDegrees, expectedPrecision);
        }
    }

    public class Given_zero_radian
    {
        [Fact]
        public void Should_return_positive_infinite()
        {
            // Arrange
            double radian = 0;
            double expectedDegrees = 0;

            // Act
            var actualDegrees = Helper.RadiansToDegrees(radian);

            // Assert
            actualDegrees.Should().BeApproximately(expectedDegrees, double.PositiveInfinity);
        }
    }
}
