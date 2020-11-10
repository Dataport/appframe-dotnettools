using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    public class FilteringTests
    {
        [Fact]
        public void Distinct_SimpleType_ShouldWork()
        {
            // arrange
            var a = new DistinctClass { SomeNumber = 1 };
            var b = new DistinctClass { SomeNumber = 1 };
            var c = new DistinctClass { SomeNumber = 2 };
            var list = new List<DistinctClass> { a, b, c };

            // act
            var result = list.Distinct(l => l.SomeNumber).ToList();

            // assert
            result.Should().HaveCount(2);
            result.Should().Contain(a);
            result.Should().Contain(c);
        }

        [Fact]
        public void Distinct_EquatableType_ShouldWork()
        {
            // arrange
            var a = new DistinctClass { C = new DistinctClass.Comparable { B = "a" } };
            var b = new DistinctClass { C = new DistinctClass.Comparable { B = "a" } };
            var c = new DistinctClass { C = new DistinctClass.Comparable { B = "b" } };
            var list = new List<DistinctClass> { a, b, c };

            // act
            var result = list.Distinct(l => l.C).ToList();

            // assert
            result.Should().HaveCount(2);
            result.Should().Contain(a);
            result.Should().Contain(c);
        }

        [Fact]
        public void Distinct_NotEquatableType_ShouldNotBreak()
        {
            // arrange
            var a = new DistinctClass { Nc = new DistinctClass.NotComparable { A = "a" } };
            var b = new DistinctClass { Nc = new DistinctClass.NotComparable { A = "a" } };
            var c = new DistinctClass { Nc = new DistinctClass.NotComparable { A = "b" } };
            var list = new List<DistinctClass> { a, b, c };

            // act
            var result = list.Distinct(l => l.Nc).ToList();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(a);
            result.Should().Contain(b);
            result.Should().Contain(c);
        }

        [Fact]
        public void WhereNotNull_EmptyCollection_ReturnsEmptyCollection()
        {
            // arrange
            var collection = Enumerable.Empty<string>();

            // act
            var result = collection.WhereNotNull();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void WhereNotNull_CollectionWithOnlyNull_ReturnsEmptyCollection()
        {
            // arrange
            var collection = new string[] { null, null, null };

            // act
            var result = collection.WhereNotNull();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void WhereNotNull_MixedCollection_FiltersNullValues()
        {
            // arrange
            var collection = new[] { "das", null, "ist", null, "ein", null, "test" };

            // act
            var result = collection.WhereNotNull().ToArray();

            // assert
            result.Should().HaveCount(4);
            result.Should().Contain("das");
            result.Should().Contain("ist");
            result.Should().Contain("ein");
            result.Should().Contain("test");
        }

        [Theory]
        [InlineData(null, '*')]
        [InlineData("", '*')]
        [InlineData("*aa*", '*', "lmnaa")]
        [InlineData("*a*", '*', "abcd", "lmnaa")]
        [InlineData("a*", '*', "abcd")]
        [InlineData("*a", '*', "lmnaa")]
        [InlineData("*a", '#')]
        [InlineData("efg", '*', "efg")]
        [InlineData("efg", '+', "efg")]
        public void WhereLike_EnumerableAndStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params string[] expected)
        {
            // arrange
            var collection = new[] { "abcd", "efg", "hijk", "lmnaa" };

            // act
            var result = collection.WhereLike(filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*')]
        [InlineData("", '*')]
        [InlineData("*7*", '*', 272)]
        [InlineData("*1*", '*', 12, 13, 14, 15)]
        [InlineData("2*", '*', 22, 272)]
        [InlineData("*2", '*', 12, 22, 272)]
        [InlineData("*a", '#')]
        [InlineData("15", '*', 15)]
        [InlineData("15", '+', 15)]
        public void WhereLike_EnumerableAndNotStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params int[] expected)
        {
            // arrange
            var collection = new[] { 12, 13, 14, 15, 22, 272 };

            // act
            var result = collection.WhereLike(x => x.ToString(), filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*')]
        [InlineData("", '*')]
        [InlineData("*aa*", '*', "lmnaa")]
        [InlineData("*a*", '*', "abcd", "lmnaa")]
        [InlineData("a*", '*', "abcd")]
        [InlineData("*a", '*', "lmnaa")]
        [InlineData("*a", '#')]
        [InlineData("efg", '*', "efg")]
        [InlineData("efg", '+', "efg")]
        public void WhereLike_QueryableAndStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params string[] expected)
        {
            // arrange
            var collection = new[] { "abcd", "efg", "hijk", "lmnaa" }.AsQueryable();

            // act
            var result = collection.WhereLike(filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*')]
        [InlineData("", '*')]
        [InlineData("*7*", '*', 272)]
        [InlineData("*1*", '*', 12, 13, 14, 15)]
        [InlineData("2*", '*', 22, 272)]
        [InlineData("*2", '*', 12, 22, 272)]
        [InlineData("*a", '#')]
        [InlineData("15", '*', 15)]
        [InlineData("15", '+', 15)]
        public void WhereLike_QueryableAndNotStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params int[] expected)
        {
            // arrange
            var collection = new[] { 12, 13, 14, 15, 22, 272 }.AsQueryable();

            // act
            var result = collection.WhereLike(x => x.ToString(), filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*', "abcd", "efg", "hijk", "lmnaa")]
        [InlineData("", '*', "abcd", "efg", "hijk", "lmnaa")]
        [InlineData("*aa*", '*', "lmnaa")]
        [InlineData("*a*", '*', "abcd", "lmnaa")]
        [InlineData("a*", '*', "abcd")]
        [InlineData("*a", '*', "lmnaa")]
        [InlineData("*a", '#')]
        [InlineData("efg", '*', "efg")]
        [InlineData("efg", '+', "efg")]
        public void WhereLikeOptional_EnumerableAndStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params string[] expected)
        {
            // arrange
            var collection = new[] { "abcd", "efg", "hijk", "lmnaa" };

            // act
            var result = collection.WhereLikeOptional(filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*', 12, 13, 14, 15, 22, 272)]
        [InlineData("", '*', 12, 13, 14, 15, 22, 272)]
        [InlineData("*7*", '*', 272)]
        [InlineData("*1*", '*', 12, 13, 14, 15)]
        [InlineData("2*", '*', 22, 272)]
        [InlineData("*2", '*', 12, 22, 272)]
        [InlineData("*a", '#')]
        [InlineData("15", '*', 15)]
        [InlineData("15", '+', 15)]
        public void WhereLikeOptional_EnumerableAndNotStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params int[] expected)
        {
            // arrange
            var collection = new[] { 12, 13, 14, 15, 22, 272 };

            // act
            var result = collection.WhereLikeOptional(x => x.ToString(), filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*', "abcd", "efg", "hijk", "lmnaa")]
        [InlineData("", '*', "abcd", "efg", "hijk", "lmnaa")]
        [InlineData("*aa*", '*', "lmnaa")]
        [InlineData("*a*", '*', "abcd", "lmnaa")]
        [InlineData("a*", '*', "abcd")]
        [InlineData("*a", '*', "lmnaa")]
        [InlineData("*a", '#')]
        [InlineData("efg", '*', "efg")]
        [InlineData("efg", '+', "efg")]
        public void WhereLikeOptional_QueryableAndStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params string[] expected)
        {
            // arrange
            var collection = new[] { "abcd", "efg", "hijk", "lmnaa" }.AsQueryable();

            // act
            var result = collection.WhereLikeOptional(filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null, '*', 12, 13, 14, 15, 22, 272)]
        [InlineData("", '*', 12, 13, 14, 15, 22, 272)]
        [InlineData("*7*", '*', 272)]
        [InlineData("*1*", '*', 12, 13, 14, 15)]
        [InlineData("2*", '*', 22, 272)]
        [InlineData("*2", '*', 12, 22, 272)]
        [InlineData("*a", '#')]
        [InlineData("15", '*', 15)]
        [InlineData("15", '+', 15)]
        public void WhereLikeOptional_QueryableAndNotStringEnumerableCases_ReturnsExpectedResult(string filterExpression, char wildCard, params int[] expected)
        {
            // arrange
            var collection = new[] { 12, 13, 14, 15, 22, 272 }.AsQueryable();

            // act
            var result = collection.WhereLikeOptional(x => x.ToString(), filterExpression, wildCard);

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void WhereEquals_EnumerableAndIntEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 11, 12, 13 };

            // act
            var result = collection.WhereEquals(12);

            // assert
            result.Should().ContainSingle(i => i == 12);
        }

        [Fact]
        public void WhereEquals_EnumerableAndStringEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { "abc", null, "def", "ghi" };

            // act
            var result = collection.WhereEquals("def");

            // assert
            result.Should().ContainSingle(r => r == "def");
        }

        [Fact]
        public void WhereEquals_EnumerableAndComplexStringEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new StringPropertyClass { Prop = "abc" },
                new StringPropertyClass(),
                new StringPropertyClass { Prop = "def" },
                new StringPropertyClass { Prop = "ghi" },
            };

            // act
            var result = collection.WhereEquals(c => c.Prop, "def");

            // assert
            result.Should().ContainSingle(r => r.Prop == "def");
        }

        [Fact]
        public void WhereEquals_EnumerableAndComplexIntEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new IntPropertyClass { Prop = 11 },
                new IntPropertyClass { Prop = 12 },
                new IntPropertyClass { Prop = 13 },
            };

            // act
            var result = collection.WhereEquals(c => c.Prop, 12);

            // assert
            result.Should().ContainSingle(r => r.Prop == 12);
        }

        [Fact]
        public void WhereEquals_QueryableAndIntEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 11, 12, 13 }.AsQueryable();

            // act
            var result = collection.WhereEquals(12);

            // assert
            result.Should().ContainSingle(i => i == 12);
        }

        [Fact]
        public void WhereEquals_QueryableAndStringEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { "abc", null, "def", "ghi" }.AsQueryable();

            // act
            var result = collection.WhereEquals("def");

            // assert
            result.Should().ContainSingle(r => r == "def");
        }

        [Fact]
        public void WhereEquals_QueryableAndComplexStringEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new StringPropertyClass { Prop = "abc" },
                new StringPropertyClass(),
                new StringPropertyClass { Prop = "def" },
                new StringPropertyClass { Prop = "ghi" },
            }.AsQueryable();

            // act
            var result = collection.WhereEquals(c => c.Prop, "def");

            // assert
            result.Should().ContainSingle(r => r.Prop == "def");
        }

        [Fact]
        public void WhereEquals_QueryableAndComplexIntEnumeration_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new IntPropertyClass { Prop = 11 },
                new IntPropertyClass { Prop = 12 },
                new IntPropertyClass { Prop = 13 },
            }.AsQueryable();

            // act
            var result = collection.WhereEquals(c => c.Prop, 12);

            // assert
            result.Should().ContainSingle(r => r.Prop == 12);
        }

        [Fact]
        public void WhereEqualsOptional_EnumerableAndSimpleComparableEnumerationAndNotNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 11, 12, 13 };

            // act
            var result = collection.WhereEqualsOptional(12);

            // assert
            result.Should().ContainSingle(i => i == 12);
        }

        [Fact]
        public void WhereEqualsOptional_EnumerableAndSimpleComparableEnumerationAndNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 11, 12, 13 };

            // act
            var result = collection.WhereEqualsOptional(null);

            // assert
            result.Should().BeEquivalentTo(collection);
        }

        [Fact]
        public void WhereEqualsOptional_EnumerableAndComplexEnumerationAndNotNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new IntPropertyClass {Prop = 11},
                new IntPropertyClass {Prop = 12},
                new IntPropertyClass {Prop = 13},
            };

            // act
            var result = collection.WhereEqualsOptional(c => c.Prop, 12);

            // assert
            result.Should().ContainSingle(c => c.Prop == 12);
        }

        [Fact]
        public void WhereEqualsOptional_EnumerableAndComplexEnumerationAndNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new IntPropertyClass {Prop = 11},
                new IntPropertyClass {Prop = 12},
                new IntPropertyClass {Prop = 13},
            };

            // act
            var result = collection.WhereEqualsOptional(c => c.Prop, null);

            // assert
            result.Should().BeEquivalentTo(collection.AsEnumerable());
        }

        [Fact]
        public void WhereEqualsOptional_QueryableAndSimpleComparableEnumerationAndNotNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 11, 12, 13 }.AsQueryable();

            // act
            var result = collection.WhereEqualsOptional(12);

            // assert
            result.Should().ContainSingle(i => i == 12);
        }

        [Fact]
        public void WhereEqualsOptional_QueryableAndSimpleComparableEnumerationAndNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 11, 12, 13 }.AsQueryable();

            // act
            var result = collection.WhereEqualsOptional(null);

            // assert
            result.Should().BeEquivalentTo(collection);
        }

        [Fact]
        public void WhereEqualsOptional_QueryableAndComplexEnumerationAndNotNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new IntPropertyClass {Prop = 11},
                new IntPropertyClass {Prop = 12},
                new IntPropertyClass {Prop = 13},
            }.AsQueryable();

            // act
            var result = collection.WhereEqualsOptional(c => c.Prop, 12);

            // assert
            result.Should().ContainSingle(c => c.Prop == 12);
        }

        [Fact]
        public void WhereEqualsOptional_QueryableAndComplexEnumerationAndNullValue_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new IntPropertyClass {Prop = 11},
                new IntPropertyClass {Prop = 12},
                new IntPropertyClass {Prop = 13},
            }.AsQueryable();

            // act
            var result = collection.WhereEqualsOptional(c => c.Prop, null);

            // assert
            result.Should().BeEquivalentTo(collection.AsEnumerable());
        }

        private class IntPropertyClass
        {
            public int Prop { get; set; }
        }

        private class StringPropertyClass
        {
            public string Prop { get; set; }
        }

        private class DistinctClass
        {
            public int SomeNumber { get; set; }

            public NotComparable Nc { get; set; }

            public Comparable C { get; set; }

            public class NotComparable
            {
                public string A { get; set; }
            }

            public class Comparable : IEquatable<Comparable>, IComparable<Comparable>, IComparable
            {
                public string B { get; set; }

                public bool Equals(Comparable other)
                {
                    return B == other?.B;
                }

                public int CompareTo(Comparable other)
                {
                    return string.Compare(B, other?.B, StringComparison.InvariantCulture);
                }

                public int CompareTo(object obj)
                {
                    return CompareTo(obj as Comparable);
                }
            }
        }
    }
}