using Dataport.AppFrameDotNet.DotNetTools.Collections;
using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public class CheckingTests
    {
        [Fact]
        public void IsNullOrEmpty_NullGiven_ReturnsTrue()
        {
            // arrange
            IList<string> collection = null;

            // act
            var result = collection.IsNullOrEmpty();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmpty_EmptyGiven_ReturnsTrue()
        {
            // arrange
            var collection = new List<string>();

            // act
            var result = collection.IsNullOrEmpty();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmpty_EmptyStringGiven_ReturnsTrue()
        {
            // arrange
            var collection = string.Empty;

            // act
            var result = collection.IsNullOrEmpty();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmpty_WhitespaceStringGiven_ReturnsFalse()
        {
            // arrange
            var collection = " ";

            // act
            var result = collection.IsNullOrEmpty();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsNullOrEmpty_FilledGiven_ReturnsFalse()
        {
            // arrange
            var collection = new List<string> { "abc" };

            // act
            var result = collection.IsNullOrEmpty();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsNullOrEmpty_ConsumingEnumerableGiven_BehavesCorrectly()
        {
            // arrange
            var empty = new NonRepeatableEnumerable<string>(new List<string>());
            var filled = new NonRepeatableEnumerable<string>(new List<string> { "abc", "def" });

            // act
            var resultEmpty = empty.IsNullOrEmpty();
            var resultFilled = filled.IsNullOrEmpty();

            // assert
            resultEmpty.Should().BeTrue();
            resultFilled.Should().BeFalse();
        }

        [Fact]
        public void ContainOneOf_NullGiven_ReturnsFalse()
        {
            // arrange
            IList<string> collection = null;

            // act
            var result = collection.ContainOneOf("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainOneOf_EmptyGiven_ReturnsFalse()
        {
            // arrange
            var collection = new List<string>();

            // act
            var result = collection.ContainOneOf("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainOneOf_NotMatching_ReturnsFalse()
        {
            // arrange
            var collection = new List<string> { "ghi" };

            // act
            var result = collection.ContainOneOf("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainOneOf_Matching_ReturnsTrue()
        {
            // arrange
            var collection = new List<string> { "def", "jkl" };

            // act
            var result = collection.ContainOneOf("abc", "def");

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ContainOneOf_ConsumingEnumerableGiven_BehavesCorrectly()
        {
            // arrange
            var matching = new NonRepeatableEnumerable<string>(new List<string> { "abc", "dev", "ghi" });
            var notMatching = new NonRepeatableEnumerable<string>(new List<string> { "abc", "ghi" });

            // act
            var matchingResult = matching.ContainOneOf("hi", "dev");
            var notMatchingResult = notMatching.ContainOneOf("hi", "dev");

            // assert
            matchingResult.Should().BeTrue();
            notMatchingResult.Should().BeFalse();
        }

        [Fact]
        public void ContainAll_NullGiven_ReturnsFalse()
        {
            // arrange
            IList<string> collection = null;

            // act
            var result = collection.ContainAll("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainAll_EmptyGiven_ReturnsFalse()
        {
            // arrange
            var collection = new List<string>();

            // act
            var result = collection.ContainAll("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainAll_NotMatching_ReturnsFalse()
        {
            // arrange
            var collection = new List<string> { "ghi" };

            // act
            var result = collection.ContainAll("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainAll_PartialMatching_ReturnsFalse()
        {
            // arrange
            var collection = new List<string> { "def", "ghi" };

            // act
            var result = collection.ContainAll("abc", "def");

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ContainAll_Matching_ReturnsTrue()
        {
            // arrange
            var collection = new List<string> { "abc", "def", "jkl" };

            // act
            var result = collection.ContainAll("abc", "def");

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ContainAll_ConsumingEnumerableGiven_BehavesCorrectly()
        {
            // arrange
            var matching = new NonRepeatableEnumerable<string>(new List<string> { "abc", "dev", "ghi" });
            var notMatching = new NonRepeatableEnumerable<string>(new List<string> { "abc", "ghi" });

            // act
            var matchingResult = matching.ContainAll("ghi", "dev");
            var notMatchingResult = notMatching.ContainAll("ghi", "dev");

            // assert
            matchingResult.Should().BeTrue();
            notMatchingResult.Should().BeFalse();
        }
    }
}