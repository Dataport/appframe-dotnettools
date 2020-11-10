using Dataport.AppFrameDotNet.DotNetTools.Comparison.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Comparison.Model;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Comparison.Extensions
{
    public class ComparisonExtensionsTests
    {
        [Theory]
        [InlineData(2, 1, 3, BoundaryType.Exclusive, true)]
        [InlineData(2, 2, 2, BoundaryType.Exclusive, false)]
        [InlineData(2, 2, 3, BoundaryType.Exclusive, false)]
        [InlineData(2, 1, 2, BoundaryType.Exclusive, false)]
        [InlineData(2, -2, 1, BoundaryType.Exclusive, false)]
        [InlineData(2, 3, 6, BoundaryType.Exclusive, false)]
        [InlineData(2, 1, 3, BoundaryType.Inclusive, true)]
        [InlineData(2, 1, 2, BoundaryType.Inclusive, true)]
        [InlineData(2, 2, 3, BoundaryType.Inclusive, true)]
        [InlineData(2, -2, 1, BoundaryType.Inclusive, false)]
        [InlineData(2, 3, 6, BoundaryType.Inclusive, false)]
        [InlineData(2, 1, 3, BoundaryType.LowerOnly, true)]
        [InlineData(2, 2, 3, BoundaryType.LowerOnly, true)]
        [InlineData(2, -2, 2, BoundaryType.LowerOnly, false)]
        [InlineData(2, 3, 6, BoundaryType.LowerOnly, false)]
        [InlineData(2, 1, 3, BoundaryType.UpperOnly, true)]
        [InlineData(2, 1, 2, BoundaryType.UpperOnly, true)]
        [InlineData(2, -2, 1, BoundaryType.UpperOnly, false)]
        [InlineData(2, 3, 6, BoundaryType.UpperOnly, false)]
        public void IsInRange_WithValues_ReturnsExpected(IComparable value, IComparable lower, IComparable upper, BoundaryType type, bool expectedResult)
        {
            // act
            var result = value.IsInRange(lower, upper, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void IsInRange_LowerBoundaryHigherThanHigherBoundary_ThrowsException()
        {
            // arrange
            Action fail = () => 2.IsInRange(3, 1);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [ClassData(typeof(WithInTestValueProvider))]
        public void IsWithin_WithValues_ReturnsExpected(IComparable value, IComparable upper, BoundaryType type, bool expectedResult)
        {
            // act
            var result = value.IsWithin(upper, type);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void IsEqualOnPropertyLevel_Equal_ReturnsTrue()
        {
            // arrange
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Zwei" };

            // act
            var result = source.IsEqualOnPropertyLevel(target, typeof(IgnoreInPropertyComparisonAttribute));

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEqualOnPropertyLevel_NotEqual_ReturnsFalse()
        {
            // arrange
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = "Hallo", Nummer = 2, Egal = "Zwei" };

            // act
            var result = source.IsEqualOnPropertyLevel(target, typeof(IgnoreInPropertyComparisonAttribute));

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEqualOnPropertyLevel_NotEqual_WithResult()
        {
            // arrange
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = "Hallo", Nummer = 2, Egal = "Zwei" };
            MemberComparisonResult[] results = null;

            // act
            var result = source.IsEqualOnPropertyLevel(target, ref results, typeof(IgnoreInPropertyComparisonAttribute));

            // assert
            result.Should().BeFalse();
            results.Should().NotBeNull();
            results.Should().HaveCount(1);
            results[0].MemberName.Should().Be("Nummer");
            results[0].SourceValue.Should().Be(1);
            results[0].TargetValue.Should().Be(2);
        }

        private class Testklasse
        {
            public string Text { get; set; }

            public int Nummer { get; set; }

            [IgnoreInPropertyComparison]
            public string Egal { get; set; }
        }

        private class WithInTestValueProvider : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "abc", "abc", BoundaryType.Inclusive, true };
                yield return new object[] { "abc", "abcd", BoundaryType.Exclusive, true };
                yield return new object[] { "abc", "abc", BoundaryType.Exclusive, false };
                yield return new object[] { "abc", "abc", BoundaryType.LowerOnly, false };
                yield return new object[] { "abc", "abc", BoundaryType.UpperOnly, true };
                yield return new object[] { -3, -3, BoundaryType.Inclusive, true };
                yield return new object[] { -3, 5, BoundaryType.Exclusive, true };
                yield return new object[] { -3, -3, BoundaryType.Exclusive, false };
                yield return new object[] { -3, -3, BoundaryType.LowerOnly, false };
                yield return new object[] { -3, -3, BoundaryType.UpperOnly, true };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 1), BoundaryType.Inclusive, true };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 2), BoundaryType.Exclusive, true };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 1), BoundaryType.Exclusive, false };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 1), BoundaryType.LowerOnly, false };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 1), BoundaryType.UpperOnly, true };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}