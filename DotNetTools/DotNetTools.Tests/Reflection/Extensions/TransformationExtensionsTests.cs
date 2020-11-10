using System.Diagnostics.CodeAnalysis;
using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public class TransformationExtensionsTests
    {
        [Fact]
        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public void AsNullable_Default_ReturnsWrappedObject()
        {
            // arrange
            var value = 12;

            // act
            var result = 12.AsNullable();

            // assert
            result.HasValue.Should().Be(true);
            result.Value.Should().Be(value);
        }

        [Fact]
        public void AsArray_NullAndNullAsEmpty_ReturnsEmptyArray()
        {
            // arrange
            string str = null;

            // act
            var result = str.AsArray(true);

            // assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public void AsArray_NullAndNotNullAsEmpty_ReturnsArray()
        {
            // arrange
            string str = null;

            // act
            var result = str.AsArray();

            // assert
            result.Should().ContainSingle(r => r == null);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AsArray_NotNull_ReturnsArray(bool nullAsEmpty)
        {
            // arrange
            var value = 12;

            // act
            var result = value.AsArray(nullAsEmpty);

            // assert
            result.Should().ContainSingle(r => r == value);
        }

        [Fact]
        public void AsArrayOf_NullAndNullAsEmpty_ReturnsEmptyArray()
        {
            // arrange
            string str = null;

            // act
            var result = str.AsArrayOf<string, string>(true);

            // assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public void AsArrayOf_NullAndNotNullAsEmpty_ReturnsArray()
        {
            // arrange
            string str = null;

            // act
            var result = str.AsArrayOf<string, string>();

            // assert
            result.Should().ContainSingle(r => r == null);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AsArray_NotNullSameType_ReturnsArray(bool nullAsEmpty)
        {
            // arrange
            var value = 12;

            // act
            var result = value.AsArrayOf<int, int>(nullAsEmpty);

            // assert
            result.Should().ContainSingle(r => r == value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AsArray_NotNullDerivedType_ReturnsArray(bool nullAsEmpty)
        {
            // arrange
            var value = new TestClass { Foo = "test" };

            // act
            var result = value.AsArrayOf<TestClass, ITestClass>(nullAsEmpty);

            // assert
            result.Should().ContainSingle(r => r.Foo == value.Foo);
        }

        private class TestClass : ITestClass
        {
            public string Foo { get; set; }
        }

        private interface ITestClass
        {
            string Foo { get; set; }
        }
    }
}