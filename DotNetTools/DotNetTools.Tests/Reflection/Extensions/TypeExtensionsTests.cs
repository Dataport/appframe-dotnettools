using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    public class TypeExtensionsTests
    {
        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(Type))]
        [InlineData(typeof(int?))]
        [InlineData(typeof(List<string>))]
        [InlineData(typeof(IEnumerable<string>))]
        [InlineData(typeof(ISerializable))]
        public void IsNullable_NullableType_ShouldBeTrue(Type type)
        {
            // act
            var result = type.IsNullable();

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(short))]
        [InlineData(typeof(long))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(TestEnum))]
        public void IsNullable_NotNullableType_ShouldBeFalse(Type type)
        {
            // act
            var result = type.IsNullable();

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(typeof(string), typeof(IEnumerable<char>), true)]
        [InlineData(typeof(ListChild), typeof(List<string>), false)]
        [InlineData(typeof(int), typeof(IEnumerable<char>), false)]
        public void Implements_Cases_ReturnsExpectedResult(Type t, Type implementation, bool expectedResult)
        {
            // act
            var result = t.Implements(implementation);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(ListChild), typeof(List<string>), true)]
        [InlineData(typeof(string), typeof(IEnumerable<char>), false)]
        [InlineData(typeof(int), typeof(IEnumerable<char>), false)]
        public void Extends_Cases_ReturnsExpectedResult(Type t, Type extension, bool expectedResult)
        {
            // act
            var result = t.Extends(extension);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(List<string>), true)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(int), false)]
        public void IsGenericCollection_Cases_ReturnsExpectedResult(Type t, bool expectedResult)
        {
            // act
            var result = t.IsGenericCollection();

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(List<string>), true)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(int), false)]
        public void IsGenericEnumerable_Cases_ReturnsExpectedResult(Type t, bool expectedResult)
        {
            // act
            var result = t.IsGenericEnumerable();

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(string), typeof(IEnumerable<>), true)]
        [InlineData(typeof(string), typeof(IEnumerable<char>), true)]
        [InlineData(typeof(ListChild), typeof(List<>), true)]
        [InlineData(typeof(ListChild), typeof(List<string>), true)]
        [InlineData(typeof(int), typeof(IEnumerable<>), false)]
        [InlineData(typeof(int), typeof(IEnumerable<char>), false)]
        [InlineData(typeof(ListChild), typeof(List<int>), false)]
        public void IsGenericTypeOf_Cases_ReturnsExpectedResult(Type t, Type ofType, bool expectedResult)
        {
            // act
            var result = t.IsGenericTypeOf(ofType);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<char>), typeof(string), true)]
        [InlineData(typeof(IEnumerable<int>), typeof(string), false)]
        [InlineData(typeof(string), typeof(int), false)]
        public void IsGenericTypeFor_Cases_ReturnsExpectedResult(Type t, Type forType, bool expectedResult)
        {
            // act
            var result = t.IsGenericTypeFor(forType);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(string), typeof(string), true)]
        [InlineData(typeof(string), typeof(int), false)]
        [InlineData(typeof(IEnumerable<string>), typeof(IEnumerable<string>), true)]
        [InlineData(typeof(IEnumerable<string>), typeof(IEnumerable<int>), false)]
        [InlineData(typeof(List<string>), typeof(IEnumerable<string>), true)]
        [InlineData(typeof(List<string>), typeof(IEnumerable<int>), false)]
        [InlineData(typeof(ListChild), typeof(List<string>), true)]
        [InlineData(typeof(ListChild), typeof(List<int>), false)]
        [InlineData(typeof(ListChild), typeof(IEnumerable<string>), true)]
        [InlineData(typeof(ListChild), typeof(IEnumerable<int>), false)]
        [InlineData(typeof(ListChild), typeof(INestedInterface), true)]
        [InlineData(typeof(object), typeof(object), true)]
        [InlineData(typeof(object), typeof(string), false)]
        [InlineData(typeof(List<string>), typeof(ListChild), false)]
        [InlineData(typeof(IEnumerable<string>), typeof(List<string>), false)]
        public void IsOrIsInheritFrom_Cases_ReturnsExpectedResult(Type t, Type compare, bool expectedResult)
        {
            // act
            var result = t.IsOrIsInheritFrom(compare);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(nameof(MemberClass.PublicStaticProperty))]
        [InlineData(nameof(MemberClass.PublicProperty))]
        [InlineData(nameof(MemberClass.InternalProperty))]
        [InlineData("PrivateProperty")]
        public void GetPropertyByName_Cases_ReturnsProperty(string propertyName)
        {
            // act
            var result = typeof(MemberClass).GetPropertyByName(propertyName);

            // assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData(nameof(ConstantValueClass.PublicFooProp))]
        [InlineData(nameof(ConstantValueClass.PublicFooField))]
        [InlineData(nameof(ConstantValueClass.PublicFooConst))]
        public void GetConstantValue_Cases_ReturnsValue(string memberName)
        {
            // act
            var result = typeof(ConstantValueClass).GetConstantValue<string>(memberName);

            // assert
            result.Should().Be(memberName + "1");
        }

        [Fact]
        public void GetConstantValue_MemberNotExists_ThrowsArgumentException()
        {
            // arrange
            Action fail = () => typeof(ConstantValueClass).GetConstantValue<string>("Invalid");

            // assert
            fail.Should().Throw<ArgumentException>();
        }

        private class MemberClass
        {
            public static string PublicStaticProperty { get; set; }
            public string PublicProperty { get; set; }
            internal string InternalProperty { get; set; }

            [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Wird implizit benutzt")]
            private string PrivateProperty { get; set; }
        }

        private class ListChild : List<string>, IListChild
        {
        }

        private interface IListChild : INestedInterface
        {
        }

        private interface INestedInterface
        {
        }

        private enum TestEnum
        {
        }

        private class ConstantValueClass
        {
            public static string PublicFooProp { get; } = "PublicFooProp1";
            public static string PublicFooField = "PublicFooField1";
            public const string PublicFooConst = "PublicFooConst1";
        }
    }
}