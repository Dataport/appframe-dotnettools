using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("B", false)]
        [InlineData("B", true)]
        [InlineData("b", true)]
        [InlineData("1", false)]
        [InlineData("1", true)]
        [InlineData("Beh", false)]
        [InlineData("Beh", true)]
        [InlineData("beh", true)]
        public void ToEnumMember_Cases_ReturnsExpectedElement(string input, bool ignoreCase)
        {
            // act
            var result = input.ToEnumMember<TestEnum, DescriptionAttribute>(ignoreCase, a => a.Description);

            // assert
            result.Should().Be(TestEnum.B);
        }

        [Theory]
        [InlineData("b", false)]
        [InlineData("-1", true)]
        [InlineData("3", true)]
        [InlineData("D", false)]
        [InlineData("D", true)]
        [InlineData("beh", false)]
        public void ToEnumMember_InvalidInput_ThrowsException(string input, bool ignoreCase)
        {
            // arrange
            Action fail = () => input.ToEnumMember<TestEnum, DescriptionAttribute>(ignoreCase, a => a.Description);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ToEnumMember_NotUniqueAttributeConstraint_ThrowsException()
        {
            // arrange
            Action fail = () => "Zeh".ToEnumMember<TestEnum, DescriptionAttribute>(true, a => "Zeh");

            // act + assert
            fail.Should().Throw<AmbiguousMatchException>();
        }

        private enum TestEnum
        {
            [Description("Ah")]
            A,

            [Description("Beh")]
            B,

            [Description("Zeh")]
            C
        }
    }
}