using Dataport.AppFrameDotNet.DotNetTools.Validation.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Validation.Models;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Validation.Extensions
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    [SuppressMessage("ReSharper", "ThrowExceptionInUnexpectedLocation")]
    public class ConditionExtensionsTests
    {
        [Fact]
        public void IsNotNull_NullGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<string>(null, "myElement");

            Action fail = () => condition.IsNotNull();

            // act + assert
            fail.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("myElement");
        }

        [Fact]
        public void IsNotNull_NotNullGiven_ThrowsNoException()
        {
            // arrange
            var condition = new Condition<string>("fooBar", "myElement");

            Action success = () => condition.IsNotNull();

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void Is_ConditionNotMeet_ThrowsException()
        {
            // arrange
            var condition = new Condition<bool>(false, "myElement");

            Action fail = () => condition.Is(b => b);

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Be("myElement");
        }

        [Fact]
        public void Is_ConditionNotMeetAndCustomMessage_ThrowsException()
        {
            // arrange
            var condition = new Condition<bool>(false, "myElement");

            Action fail = () => condition.Is(b => b, "custom");

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Be("custom (Parameter 'myElement')");
            fail.Should().Throw<ArgumentException>().Which.ParamName.Should().Be("myElement");
        }

        [Fact]
        public void Is_ConditionMeet_ThrowsNoException()
        {
            // arrange
            var condition = new Condition<bool>(true, "myElement");

            Action success = () => condition.Is(b => b);

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void IsNullOrEmpty_NullGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<IEnumerable<int>>(null, "myElement");

            Action fail = () => condition.IsNotNullOrEmpty();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("must be present");
        }

        [Fact]
        public void IsNullOrEmpty_EmptyGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<IEnumerable<int>>(Enumerable.Empty<int>(), "myElement");

            Action fail = () => condition.IsNotNullOrEmpty();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("empty enumeration");
        }

        [Fact]
        public void IsNullOrEmpty_NotEmptyGiven_ThrowsNoException()
        {
            // arrange
            var condition = new Condition<IEnumerable<int>>(new[] { 1, 2, 3 }, "myElement");

            Action success = () => condition.IsNotNullOrEmpty();

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void IsNotNullOrWhiteSpace_NullGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<string>(null, "myElement");

            Action fail = () => condition.IsNotNullOrWhiteSpace();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("null or whitespace");
        }

        [Fact]
        public void IsNotNullOrWhiteSpace_WhiteSpaceGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<string>("   ", "myElement");

            Action fail = () => condition.IsNotNullOrWhiteSpace();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("null or whitespace");
        }

        [Fact]
        public void IsNotNullOrWhiteSpace_CorrectStringGiven_ThrowsNoException()
        {
            // arrange
            var condition = new Condition<string>("fooBar", "myElement");

            Action success = () => condition.IsNotNullOrWhiteSpace();

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void ContainsElements_EmptyGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<IEnumerable<int>>(Enumerable.Empty<int>(), "myElement");

            Action fail = () => condition.ContainsElements();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("empty enumeration");
        }

        [Fact]
        public void ContainsElements_NotEmptyGiven_ThrowsNoException()
        {
            // arrange
            var condition = new Condition<IEnumerable<int>>(new[] { 1, 2, 3 }, "myElement");

            Action success = () => condition.ContainsElements();

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void IsDefined_UndefinedGiven_ThrowsException()
        {
            // arrange
            var condition = new Condition<TestEnum>((TestEnum)4, "myElement");

            Action fail = () => condition.IsDefined();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("is not defined");
        }

        [Fact]
        public void IsDefined_DefinedGiven_ThrowsNoException()
        {
            // arrange
            var condition = new Condition<TestEnum>(TestEnum.B, "myElement");

            Action success = () => condition.IsDefined();

            // act + assert
            success.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(IsEqualToValidCases))]
        public void IsEqualTo_ValidCases_DoesNotThrowException(object a, object b)
        {
            // arrange
            var condition = new Condition<object>(a, "a");

            Action success = () => condition.IsEqualTo(b);

            // act + assert
            success.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(IsEqualToInvalidCases))]
        public void IsEqualTo_InvalidCases_DoesNotThrowException<TType>(TType a, TType b)
        {
            // arrange
            var condition = new Condition<object>(a, "a");

            Action fail = () => condition.IsEqualTo(b);

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("is not equal to");
        }

        [Fact]
        public void AllElementsNotNull_CollectionIsNull_ThrowsException()
        {
            // arrange
            IEnumerable<string> collection = null;
            var condition = new Condition<IEnumerable<string>>(collection, "c");

            Action fail = () => condition.AllElementsNotNull();

            // act + assert
            fail.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AllElementsNotNull_CollectionHasNullElements_ThrowsException()
        {
            // arrange
            var collection = new[] { "abc", "def", null, "jkl" };
            var condition = new Condition<IEnumerable<string>>(collection, "c");

            Action fail = () => condition.AllElementsNotNull();

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain("null-values");
        }

        [Fact]
        public void AllElementsNotNull_CollectionHasNoNullElements_ThrowsNoException()
        {
            // arrange
            var collection = new[] { "abc", "def", "ghi", "jkl" };
            var condition = new Condition<IEnumerable<string>>(collection, "c");

            Action success = () => condition.AllElementsNotNull();

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void HasCount_CollectionIsNull_ThrowsException()
        {
            // arrange
            IEnumerable<int> collection = null;
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action fail = () => condition.HasCount(2);

            // act + assert
            fail.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        public void HasCount_CollectionHasNoExpectedElementCount_ThrowsException(int expected)
        {
            // arrange
            var collection = new[] { 1, 2, 3 };
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action fail = () => condition.HasCount(expected);

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain($"should have count {expected} but had 3");
        }

        [Fact]
        public void HasCount_CollectionHasExpectedSize_ThrowsNoException()
        {
            // arrange
            var collection = new[] { 1, 2, 3 };
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action success = () => condition.HasCount(3);

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void HasMinimumSize_CollectionIsNull_ThrowsException()
        {
            // arrange
            IEnumerable<int> collection = null;
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action fail = () => condition.HasMinimumSize(2);

            // act + assert
            fail.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void HasMinimumSize_CollectionHasLessElements_ThrowsException(int expected)
        {
            // arrange
            var collection = new[] { 1, 2, 3 };
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action fail = () => condition.HasMinimumSize(expected);

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain($"at least have {expected} elements but had 3");
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(0)]
        [InlineData(3)]
        public void HasMinimumSize_CollectionHasExpectedSize_ThrowsNoException(int expected)
        {
            // arrange
            var collection = new[] { 1, 2, 3 };
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action success = () => condition.HasMinimumSize(expected);

            // act + assert
            success.Should().NotThrow();
        }

        [Fact]
        public void HasMaximumSize_CollectionIsNull_ThrowsException()
        {
            // arrange
            IEnumerable<int> collection = null;
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action fail = () => condition.HasMaximumSize(2);

            // act + assert
            fail.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(2)]
        public void HasMaximumSize_CollectionHasLessElements_ThrowsException(int expected)
        {
            // arrange
            var collection = new[] { 1, 2, 3 };
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action fail = () => condition.HasMaximumSize(expected);

            // act + assert
            fail.Should().Throw<ArgumentException>().Which.Message.Should().Contain($"shouldn't have more than {expected} elements but had 3");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(20)]
        public void HasMaximumSize_CollectionHasExpectedSize_ThrowsNoException(int expected)
        {
            // arrange
            var collection = new[] { 1, 2, 3 };
            var condition = new Condition<IEnumerable<int>>(collection, "c");

            Action success = () => condition.HasMaximumSize(expected);

            // act + assert
            success.Should().NotThrow();
        }

        private enum TestEnum
        {
            A,
            B,
            C
        }

        private class NotComparableClass
        {
            public string A { get; set; }
        }

        private class ComparableClass : IEquatable<ComparableClass>
        {
            public string A { get; set; }

            public int B { get; set; }

            public bool Equals(ComparableClass other)
            {
                return A == other?.A && B == other?.B;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as ComparableClass);
            }

            public override int GetHashCode()
            {
                throw new NotSupportedException();
            }
        }

        private class IsEqualToValidCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null, null };
                yield return new object[] { 12, 12 };
                yield return new object[] { "test", "test" };
                yield return new object[] { new DateTime(2020, 8, 20), new DateTime(2020, 8, 20) };
                yield return new object[] { TestEnum.C, TestEnum.C };
                yield return new object[] { new ComparableClass { A = "test", B = 12 }, new ComparableClass { A = "test", B = 12 } };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class IsEqualToInvalidCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null, "test" };
                yield return new object[] { "test", null };
                yield return new object[] { 12, 13 };
                yield return new object[] { "test", "test2" };
                yield return new object[] { new DateTime(2020, 8, 20), new DateTime(2020, 8, 22) };
                yield return new object[] { TestEnum.B, TestEnum.C };
                yield return new object[] { new ComparableClass { A = "test", B = 13 }, new ComparableClass { A = "test", B = 12 } };
                yield return new object[] { new NotComparableClass { A = "test" }, new NotComparableClass { A = "test" } };
                yield return new object[] { new NotComparableClass { A = "test" }, new NotComparableClass { A = "test2" } };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}