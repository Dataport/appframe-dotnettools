using Dataport.AppFrameDotNet.DotNetTools.Numeric.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Numeric.Extensions
{
    public class StruktureTests
    {
        [Theory]
        [InlineData(123, 3)]
        [InlineData(6, 1)]
        [InlineData(-55, 2)]
        [InlineData(0, 0)]
        public void CountDigits_ShortCases_ReturnsExpectedResult(short i, int expected)
        {
            // act
            var result = i.CountDigits();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(123, 3)]
        [InlineData(6, 1)]
        [InlineData(-55, 2)]
        [InlineData(0, 0)]
        public void CountDigits_IntCases_ReturnsExpectedResult(int i, int expected)
        {
            // act
            var result = i.CountDigits();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(123, 3)]
        [InlineData(6, 1)]
        [InlineData(-55, 2)]
        [InlineData(0, 0)]
        public void CountDigits_LongCases_ReturnsExpectedResult(long i, int expected)
        {
            // act
            var result = i.CountDigits();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(12.345f, 2)]
        [InlineData(-341.67f, 3)]
        [InlineData(0.45f, 0)]
        [InlineData(-0.78f, 0)]
        public void CountDigits_FloatCases_ReturnsExpectedResult(float i, int expected)
        {
            // act
            var result = i.CountDigits();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(12.345d, 2)]
        [InlineData(-341.67d, 3)]
        [InlineData(0.45d, 0)]
        [InlineData(-0.78d, 0)]
        public void CountDigits_DoubleCases_ReturnsExpectedResult(double i, int expected)
        {
            // act
            var result = i.CountDigits();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(12.345, 2)]
        [InlineData(-341.67, 3)]
        [InlineData(0.45, 0)]
        [InlineData(-0.78, 0)]
        public void CountDigits_DecimalCases_ReturnsExpectedResult(decimal i, int expected)
        {
            // act
            var result = i.CountDigits();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(12, 0)]
        [InlineData(-12, 0)]
        [InlineData(12.00f, 0)]
        [InlineData(-12.00f, 0)]
        [InlineData(12.340f, 2)]
        [InlineData(-12.34678f, 5)]
        public void CountDigitsDecimal_FloatCases_ReturnsExpectedResult(float i, int expected)
        {
            // act
            var result = i.CountDigitsDecimal();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(12, 0)]
        [InlineData(-12, 0)]
        [InlineData(12.00d, 0)]
        [InlineData(-12.00d, 0)]
        [InlineData(12.340d, 2)]
        [InlineData(-12.34678d, 5)]
        public void CountDigitsDecimal_DoubleCases_ReturnsExpectedResult(double i, int expected)
        {
            // act
            var result = i.CountDigitsDecimal();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(12, 0)]
        [InlineData(-12, 0)]
        [InlineData(12.00, 0)]
        [InlineData(-12.00, 0)]
        [InlineData(12.340, 2)]
        [InlineData(-12.34678, 5)]
        public void CountDigitsDecimal_DecimalCases_ReturnsExpectedResult(decimal i, int expected)
        {
            // act
            var result = i.CountDigitsDecimal();

            // assert
            result.Should().Be(expected);
        }
    }
}