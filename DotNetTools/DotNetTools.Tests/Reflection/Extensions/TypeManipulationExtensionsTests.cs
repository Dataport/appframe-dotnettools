using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
    public class TypeManipulationExtensionsTests
    {
        [Fact]
        public void DowngradeType_Default_Works()
        {
            // arrange
            IList<object> values = new object[] { 1, 2, 3 };
            IEnumerable<int> valuesToAppend = new[] { 4, 5, 6 };

            // act
            var result = values.Concat(valuesToAppend.DowngradeType<int, object>()).ToArray();

            // assert
            result.Should().HaveCount(6);
            result.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6);
        }

        [Fact]
        public void UpgradeType_PossibleUpgrade_Works()
        {
            // arrange
            IList<int> values = new[] { 1, 2, 3 };
            IEnumerable<object> valuesToAppend = new object[] { 4, 5, 6 };

            // act
            var result = values.Concat(valuesToAppend.UpgradeType<object, int>()).ToArray();

            // assert
            result.Should().HaveCount(6);
            result.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6);
        }

        [Fact]
        public void UpgradeType_NotPossibleUpgrade_ThrowsException()
        {
            // arrange
            IList<int> values = new[] { 1, 2, 3 };
            IEnumerable<object> valuesToAppend = new object[] { 4, "foobar", 6 };

            Action fail = () => values.Concat(valuesToAppend.UpgradeType<object, int>()).ToArray();

            // act + assert
            fail.Should().Throw<InvalidCastException>();
        }

        [Fact]
        public void DowngradeKeyType_Default_Works()
        {
            // arrange
            var d = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = d.DowngradeKeyType<int, IComparable, string>();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(1, "A");
            result.Should().Contain(2, "B");
            result.Should().Contain(3, "C");
        }

        [Fact]
        public void UpgradeKeyType_PossibleUpgrade_Works()
        {
            // arrange
            var d = new Dictionary<IComparable, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = d.UpgradeKeyType<IComparable, int, string>();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(1, "A");
            result.Should().Contain(2, "B");
            result.Should().Contain(3, "C");
        }

        [Fact]
        public void UpgradeKeyType_NotPossibleUpgrade_ThrowsException()
        {
            // arrange
            var d = new Dictionary<IComparable, string>
            {
                {1, "A"},
                {"foo", "B"},
                {3, "C"},
            };

            Action fail = () => d.UpgradeKeyType<IComparable, int, string>();

            // assert
            fail.Should().Throw<InvalidCastException>();
        }

        [Fact]
        public void DowngradeValueType_Default_Works()
        {
            // arrange
            var d = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = d.DowngradeValueType<int, string, IComparable>();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(1, "A");
            result.Should().Contain(2, "B");
            result.Should().Contain(3, "C");
        }

        [Fact]
        public void UpgradeValueType_PossibleUpgrade_Works()
        {
            // arrange
            var d = new Dictionary<int, IComparable>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = d.UpgradeValueType<int, IComparable, string>();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(1, "A");
            result.Should().Contain(2, "B");
            result.Should().Contain(3, "C");
        }

        [Fact]
        public void UpgradeValueType_NotPossibleUpgrade_ThrowsException()
        {
            // arrange
            var d = new Dictionary<int, IComparable>
            {
                {1, "A"},
                {2, 456},
                {3, "C"},
            };

            Action fail = () => d.UpgradeValueType<int, IComparable, string>();

            // assert
            fail.Should().Throw<InvalidCastException>();
        }
    }
}