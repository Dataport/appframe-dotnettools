using Dataport.AppFrameDotNet.DotNetTools.Numeric.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Numeric.Model;
using FluentAssertions;
using System;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Numeric.Extensions
{
    public class PercentageTests
    {
        [Theory]
        [InlineData(3, 5, PercentageType.Arithmetic, 0.6f)]
        [InlineData(0, 8, PercentageType.Arithmetic, 0)]
        [InlineData(5, 2, PercentageType.Arithmetic, 2.5f)]
        [InlineData(3, 5, PercentageType.HumanReadable, 60)]
        [InlineData(0, 8, PercentageType.HumanReadable, 0)]
        [InlineData(5, 2, PercentageType.HumanReadable, 250)]
        public void AsPercentageOf_ShortCases_ReturnsExpectedResult(short i, short reference, PercentageType type, float expectedResult)
        {
            // act
            var result = i.AsPercentageOf(reference, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 0)]
        public void AsPercentageOf_InvalidShortCases_ThrowsException(short i, short reference)
        {
            // arrange
            Action fail = () => i.AsPercentageOf(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5, 0.4f, PercentageType.Arithmetic, 2)]
        [InlineData(8, 0, PercentageType.Arithmetic, 0)]
        [InlineData(5, 0.5f, PercentageType.Arithmetic, 2.5f)]
        [InlineData(0, 0.7f, PercentageType.Arithmetic, 0)]
        [InlineData(8, 2, PercentageType.Arithmetic, 16)]
        [InlineData(5, 40, PercentageType.HumanReadable, 2)]
        [InlineData(8, 0, PercentageType.HumanReadable, 0)]
        [InlineData(5, 50, PercentageType.HumanReadable, 2.5f)]
        [InlineData(0, 70, PercentageType.HumanReadable, 0)]
        [InlineData(8, 200, PercentageType.HumanReadable, 16)]
        public void GetPercentageAmount_ShortCases_ReturnsExpectedResult(short i, float percentage, PercentageType type, float expectedResult)
        {
            // act
            var result = i.GetPercentageAmount(percentage, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GetPercentageAmount_InvalidShortCases_ThrowsException(short i, float reference)
        {
            // arrange
            Action fail = () => i.GetPercentageAmount(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(3, 5, PercentageType.Arithmetic, 0.6f)]
        [InlineData(0, 8, PercentageType.Arithmetic, 0)]
        [InlineData(5, 2, PercentageType.Arithmetic, 2.5f)]
        [InlineData(3, 5, PercentageType.HumanReadable, 60)]
        [InlineData(0, 8, PercentageType.HumanReadable, 0)]
        [InlineData(5, 2, PercentageType.HumanReadable, 250)]
        public void AsPercentageOf_IntCases_ReturnsExpectedResult(int i, int reference, PercentageType type, float expectedResult)
        {
            // act
            var result = i.AsPercentageOf(reference, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 0)]
        public void AsPercentageOf_InvalidIntCases_ThrowsException(int i, int reference)
        {
            // arrange
            Action fail = () => i.AsPercentageOf(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5, 0.4f, PercentageType.Arithmetic, 2)]
        [InlineData(8, 0, PercentageType.Arithmetic, 0)]
        [InlineData(5, 0.5f, PercentageType.Arithmetic, 2.5f)]
        [InlineData(0, 0.7f, PercentageType.Arithmetic, 0)]
        [InlineData(8, 2, PercentageType.Arithmetic, 16)]
        [InlineData(5, 40, PercentageType.HumanReadable, 2)]
        [InlineData(8, 0, PercentageType.HumanReadable, 0)]
        [InlineData(5, 50, PercentageType.HumanReadable, 2.5f)]
        [InlineData(0, 70, PercentageType.HumanReadable, 0)]
        [InlineData(8, 200, PercentageType.HumanReadable, 16)]
        public void GetPercentageAmount_IntCases_ReturnsExpectedResult(int i, float percentage, PercentageType type, float expectedResult)
        {
            // act
            var result = i.GetPercentageAmount(percentage, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GetPercentageAmount_InvalidIntCases_ThrowsException(int i, float reference)
        {
            // arrange
            Action fail = () => i.GetPercentageAmount(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(3, 5, PercentageType.Arithmetic, 0.6f)]
        [InlineData(0, 8, PercentageType.Arithmetic, 0)]
        [InlineData(5, 2, PercentageType.Arithmetic, 2.5f)]
        [InlineData(3, 5, PercentageType.HumanReadable, 60)]
        [InlineData(0, 8, PercentageType.HumanReadable, 0)]
        [InlineData(5, 2, PercentageType.HumanReadable, 250)]
        public void AsPercentageOf_LongCases_ReturnsExpectedResult(long i, long reference, PercentageType type, float expectedResult)
        {
            // act
            var result = i.AsPercentageOf(reference, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 0)]
        public void AsPercentageOf_InvalidLongCases_ThrowsException(long i, long reference)
        {
            // arrange
            Action fail = () => i.AsPercentageOf(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5, 0.4f, PercentageType.Arithmetic, 2)]
        [InlineData(8, 0, PercentageType.Arithmetic, 0)]
        [InlineData(5, 0.5f, PercentageType.Arithmetic, 2.5f)]
        [InlineData(0, 0.7f, PercentageType.Arithmetic, 0)]
        [InlineData(8, 2, PercentageType.Arithmetic, 16)]
        [InlineData(5, 40, PercentageType.HumanReadable, 2)]
        [InlineData(8, 0, PercentageType.HumanReadable, 0)]
        [InlineData(5, 50, PercentageType.HumanReadable, 2.5f)]
        [InlineData(0, 70, PercentageType.HumanReadable, 0)]
        [InlineData(8, 200, PercentageType.HumanReadable, 16)]
        public void GetPercentageAmount_LongCases_ReturnsExpectedResult(long i, float percentage, PercentageType type, float expectedResult)
        {
            // act
            var result = i.GetPercentageAmount(percentage, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GetPercentageAmount_InvalidLongCases_ThrowsException(long i, float reference)
        {
            // arrange
            Action fail = () => i.GetPercentageAmount(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(3, 5, PercentageType.Arithmetic, 0.6f)]
        [InlineData(0, 8, PercentageType.Arithmetic, 0)]
        [InlineData(5, 2, PercentageType.Arithmetic, 2.5f)]
        [InlineData(3, 5, PercentageType.HumanReadable, 60)]
        [InlineData(0, 8, PercentageType.HumanReadable, 0)]
        [InlineData(5, 2, PercentageType.HumanReadable, 250)]
        public void AsPercentageOf_FloatCases_ReturnsExpectedResult(float i, float reference, PercentageType type, float expectedResult)
        {
            // act
            var result = i.AsPercentageOf(reference, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 0)]
        public void AsPercentageOf_InvalidFloatCases_ThrowsException(float i, float reference)
        {
            // arrange
            Action fail = () => i.AsPercentageOf(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5, 0.4f, PercentageType.Arithmetic, 2)]
        [InlineData(8, 0, PercentageType.Arithmetic, 0)]
        [InlineData(5, 0.5f, PercentageType.Arithmetic, 2.5f)]
        [InlineData(0, 0.7f, PercentageType.Arithmetic, 0)]
        [InlineData(8.1f, 2, PercentageType.Arithmetic, 16.2f)]
        [InlineData(5, 40, PercentageType.HumanReadable, 2)]
        [InlineData(8, 0, PercentageType.HumanReadable, 0)]
        [InlineData(5, 50, PercentageType.HumanReadable, 2.5f)]
        [InlineData(0, 70, PercentageType.HumanReadable, 0)]
        [InlineData(8.1f, 200, PercentageType.HumanReadable, 16.2f)]
        public void GetPercentageAmount_FloatCases_ReturnsExpectedResult(float i, float percentage, PercentageType type, float expectedResult)
        {
            // act
            var result = i.GetPercentageAmount(percentage, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GetPercentageAmount_InvalidFloatCases_ThrowsException(float i, float reference)
        {
            // arrange
            Action fail = () => i.GetPercentageAmount(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(3, 5, PercentageType.Arithmetic, 0.6d)]
        [InlineData(0, 8, PercentageType.Arithmetic, 0)]
        [InlineData(5, 2, PercentageType.Arithmetic, 2.5d)]
        [InlineData(3, 5, PercentageType.HumanReadable, 60)]
        [InlineData(0, 8, PercentageType.HumanReadable, 0)]
        [InlineData(5, 2, PercentageType.HumanReadable, 250)]
        public void AsPercentageOf_DoubleCases_ReturnsExpectedResult(double i, double reference, PercentageType type, double expectedResult)
        {
            // act
            var result = i.AsPercentageOf(reference, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 0)]
        public void AsPercentageOf_InvalidDoubleCases_ThrowsException(double i, double reference)
        {
            // arrange
            Action fail = () => i.AsPercentageOf(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5, 0.4d, PercentageType.Arithmetic, 2)]
        [InlineData(8, 0, PercentageType.Arithmetic, 0)]
        [InlineData(5, 0.5d, PercentageType.Arithmetic, 2.5d)]
        [InlineData(0, 0.7d, PercentageType.Arithmetic, 0)]
        [InlineData(8.1d, 2, PercentageType.Arithmetic, 16.2d)]
        [InlineData(5, 40, PercentageType.HumanReadable, 2)]
        [InlineData(8, 0, PercentageType.HumanReadable, 0)]
        [InlineData(5, 50, PercentageType.HumanReadable, 2.5d)]
        [InlineData(0, 70, PercentageType.HumanReadable, 0)]
        [InlineData(8.1d, 200, PercentageType.HumanReadable, 16.2d)]
        public void GetPercentageAmount_DoubleCases_ReturnsExpectedResult(double i, double percentage, PercentageType type, double expectedResult)
        {
            // act
            var result = i.GetPercentageAmount(percentage, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GetPercentageAmount_InvalidDoubleCases_ThrowsException(double i, float reference)
        {
            // arrange
            Action fail = () => i.GetPercentageAmount(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(3, 5, PercentageType.Arithmetic, 0.6)]
        [InlineData(0, 8, PercentageType.Arithmetic, 0)]
        [InlineData(5, 2, PercentageType.Arithmetic, 2.5)]
        [InlineData(3, 5, PercentageType.HumanReadable, 60)]
        [InlineData(0, 8, PercentageType.HumanReadable, 0)]
        [InlineData(5, 2, PercentageType.HumanReadable, 250)]
        public void AsPercentageOf_DecimalCases_ReturnsExpectedResult(decimal i, decimal reference, PercentageType type, decimal expectedResult)
        {
            // act
            var result = i.AsPercentageOf(reference, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(5, 0)]
        public void AsPercentageOf_InvalidDecimalCases_ThrowsException(decimal i, decimal reference)
        {
            // arrange
            Action fail = () => i.AsPercentageOf(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5, 0.4, PercentageType.Arithmetic, 2)]
        [InlineData(8, 0, PercentageType.Arithmetic, 0)]
        [InlineData(5, 0.5, PercentageType.Arithmetic, 2.5)]
        [InlineData(0, 0.7, PercentageType.Arithmetic, 0)]
        [InlineData(8.1, 2, PercentageType.Arithmetic, 16.2)]
        [InlineData(5, 40, PercentageType.HumanReadable, 2)]
        [InlineData(8, 0, PercentageType.HumanReadable, 0)]
        [InlineData(5, 50, PercentageType.HumanReadable, 2.5)]
        [InlineData(0, 70, PercentageType.HumanReadable, 0)]
        [InlineData(8.1, 200, PercentageType.HumanReadable, 16.2)]
        public void GetPercentageAmount_DecimalCases_ReturnsExpectedResult(decimal i, decimal percentage, PercentageType type, decimal expectedResult)
        {
            // act
            var result = i.GetPercentageAmount(percentage, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GetPercentageAmount_InvalidDecimalCases_ThrowsException(decimal i, decimal reference)
        {
            // arrange
            Action fail = () => i.GetPercentageAmount(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }
    }
}