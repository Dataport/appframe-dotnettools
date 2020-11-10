using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    public class IndexingTests
    {
        [Fact]
        public void FirstIndexOf_DoesNotContain_ReturnsMinus1()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            var result = arr.FirstIndexOf(i => i == 4);

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void FirstIndexOf_ContainsElementOnce_ReturnsIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            var result = arr.FirstIndexOf(i => i == 2);

            // assert
            result.Should().Be(1);
        }

        [Fact]
        public void FirstIndexOf_ContainsElementTwice_ReturnsFirstIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 2 };

            // act
            var result = arr.FirstIndexOf(i => i == 2);

            // assert
            result.Should().Be(1);
        }

        [Fact]
        public void FirstIndexOf_WithStringAndNotContain_ReturnsMinus1()
        {
            // arrange
            var str = "abcdefg";

            // act
            var result = str.FirstIndexOf("hi");

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void FirstIndexOf_WithStringAndContainSingle_ReturnsIndex()
        {
            // arrange
            var str = "abcdefg";

            // act
            var result = str.FirstIndexOf("de");

            // assert
            result.Should().Be(3);
        }

        [Fact]
        public void FirstIndexOf_WithStringAndContainMultiple_ReturnsFirstIndex()
        {
            // arrange
            var str = "abcdefgde";

            // act
            var result = str.FirstIndexOf("de");

            // assert
            result.Should().Be(3);
        }

        [Fact]
        public void SecondIndexOf_DoesNotContain_ReturnsMinus1()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            var result = arr.SecondIndexOf(i => i == 4);

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void SecondIndexOf_ContainsElementTwice_ReturnsIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 2 };

            // act
            var result = arr.SecondIndexOf(i => i == 2);

            // assert
            result.Should().Be(3);
        }

        [Fact]
        public void SecondIndexOf_ContainsElementMultipleTimes_ReturnsSecondIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 2, 4, 2, 6 };

            // act
            var result = arr.SecondIndexOf(i => i == 2);

            // assert
            result.Should().Be(3);
        }

        [Fact]
        public void SecondIndexOf_WithStringAndNotContain_ReturnsMinus1()
        {
            // arrange
            var str = "abcdefg";

            // act
            var result = str.SecondIndexOf("hi");

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void SecondIndexOf_WithStringAndContainTwice_ReturnsIndex()
        {
            // arrange
            var str = "abcdefgde";

            // act
            var result = str.SecondIndexOf("de");

            // assert
            result.Should().Be(7);
        }

        [Fact]
        public void SecondIndexOf_WithStringAndContainMultiple_ReturnsSecondIndex()
        {
            // arrange
            var str = "abcdefgdehidese";

            // act
            var result = str.SecondIndexOf("de");

            // assert
            result.Should().Be(7);
        }

        [Fact]
        public void ThirdIndexOf_DoesNotContain_ReturnsMinus1()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            var result = arr.ThirdIndexOf(i => i == 4);

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void ThirdIndexOf_ContainsElementThreeTimes_ReturnsIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 2, 4, 2 };

            // act
            var result = arr.ThirdIndexOf(i => i == 2);

            // assert
            result.Should().Be(5);
        }

        [Fact]
        public void ThirdIndexOf_ContainsElementMultipleTimes_ReturnsThirdIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 2, 4, 2, 6, 2, 9, 0, 2 };

            // act
            var result = arr.ThirdIndexOf(i => i == 2);

            // assert
            result.Should().Be(5);
        }

        [Fact]
        public void ThirdIndexOf_WithStringAndNotContain_ReturnsMinus1()
        {
            // arrange
            var str = "abcdefg";

            // act
            var result = str.ThirdIndexOf("hi");

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void ThirdIndexOf_WithStringAndContainThreeTimes_ReturnsIndex()
        {
            // arrange
            var str = "abcdefgdehidesf";

            // act
            var result = str.ThirdIndexOf("de");

            // assert
            result.Should().Be(11);
        }

        [Fact]
        public void ThirdIndexOf_WithStringAndContainMultiple_ReturnsThirdIndex()
        {
            // arrange
            var str = "abcdefgdehidesedeughfe";

            // act
            var result = str.ThirdIndexOf("de");

            // assert
            result.Should().Be(11);
        }

        [Fact]
        public void LastIndexOf_DoesNotContain_ReturnsMinus1()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            var result = arr.LastIndexOf(i => i == 4);

            // assert
            result.Should().Be(-1);
        }

        [Fact]
        public void LastIndexOf_ContainsElementOnce_ReturnsIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 4 };

            // act
            var result = arr.LastIndexOf(i => i == 3);

            // assert
            result.Should().Be(2);
        }

        [Fact]
        public void LastIndexOf_ContainsElementMultipleTimes_ReturnsLastIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3, 2, 4, 2, 6, 2, 9, 0, 2 };

            // act
            var result = arr.LastIndexOf(i => i == 2);

            // assert
            result.Should().Be(10);
        }

        [Fact]
        public void Second_NoConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.Second();

            // assert
            result.Should().Be(4);
        }

        [Fact]
        public void Second_NoConditionAndLessElements_ShouldThrow()
        {
            // arrange
            var elements = new List<int> { 3 };

            Action fail = () => elements.Second();

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Second_ConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.Second(i => i >= 4);

            // assert
            result.Should().Be(5);
        }

        [Fact]
        public void Second_ConditionAndLessElements_ShouldThrow()
        {
            // arrange
            var elements = new List<int> { 3, 4 };

            Action fail = () => elements.Second(i => i >= 4);

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SecondOrDefault_NoConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.SecondOrDefault();

            // assert
            result.Should().Be(4);
        }

        [Fact]
        public void SecondOrDefault_NoConditionAndLessElements_ShouldGetDefault()
        {
            // arrange
            var elements = new List<int> { 3 };

            // act
            var result = elements.SecondOrDefault();

            // assert
            result.Should().Be(0);
        }

        [Fact]
        public void SecondOrDefault_ConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.SecondOrDefault(i => i >= 4);

            // assert
            result.Should().Be(5);
        }

        [Fact]
        public void SecondOrDefault_ConditionAndLessElements_ShouldGetDefault()
        {
            // arrange
            var elements = new List<int> { 3, 4 };

            // act
            var result = elements.SecondOrDefault(i => i >= 4);

            // assert
            result.Should().Be(0);
        }

        [Fact]
        public void Third_NoConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.Third();

            // assert
            result.Should().Be(5);
        }

        [Fact]
        public void Third_NoConditionAndLessElements_ShouldThrow()
        {
            // arrange
            var elements = new List<int> { 3, 4 };

            Action fail = () => elements.Third();

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Third_ConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5, 6 };

            // act
            var result = elements.Third(i => i >= 4);

            // assert
            result.Should().Be(6);
        }

        [Fact]
        public void Third_ConditionAndLessElements_ShouldThrow()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            Action fail = () => elements.Third(i => i >= 4);

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ThirdOrDefault_NoConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.ThirdOrDefault();

            // assert
            result.Should().Be(5);
        }

        [Fact]
        public void ThirdOrDefault_NoConditionAndLessElements_ShouldGetDefault()
        {
            // arrange
            var elements = new List<int> { 3, 4 };

            // act
            var result = elements.ThirdOrDefault();

            // assert
            result.Should().Be(0);
        }

        [Fact]
        public void ThirdOrDefault_ConditionAndEnoughElements_ShouldGetResult()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5, 6 };

            // act
            var result = elements.ThirdOrDefault(i => i >= 4);

            // assert
            result.Should().Be(6);
        }

        [Fact]
        public void ThirdOrDefault_ConditionAndLessElements_ShouldGetDefault()
        {
            // arrange
            var elements = new List<int> { 3, 4, 5 };

            // act
            var result = elements.ThirdOrDefault(i => i >= 4);

            // assert
            result.Should().Be(0);
        }
    }
}