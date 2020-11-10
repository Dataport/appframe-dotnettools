using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dataport.AppFrameDotNet.DotNetTools.Collections;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections
{
    public class ExpressionHelperTests
    {
        [Theory]
        [InlineData(null, '*')]
        [InlineData("", '*')]
        [InlineData("*aa*", '*', "lmnaa")]
        [InlineData("*a*", '*', "abcd", "lmnaa")]
        [InlineData("a*", '*', "abcd")]
        [InlineData("a**", '*', "abcd")]
        [InlineData("*a", '*', "lmnaa")]
        [InlineData("*a", '#')]
        [InlineData("**a", '#')]
        [InlineData("efg", '*', "efg")]
        [InlineData("efg", '+', "efg")]
        [InlineData("ab*d", '*', "abcd")]
        [InlineData("l*n*a", '*', "lmnaa")]
        public void CreateLikeExpression_Cases_ReturnsExpectedResult(string filterExpression, char wildCard, params string[] expected)
        {
            // arrange
            var collection = new[] { "abcd", null, "efg", "hijk", "lmnaa" }.AsQueryable();

            // act
            var result = ExpressionHelper.CreateLikeExpression<string>(x => x, filterExpression, wildCard);

            // assert
            collection.Where(result).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CreateEqualExpression_DefaultIntExample_ReturnsExpectedExpression()
        {
            // arrange
            var collection = new[] { 11, 12, 13 }.AsQueryable();

            // act
            var result = ExpressionHelper.CreateEqualExpression<int, int>(i => i, 12);

            // assert
            collection.Single(result).Should().Be(12);
        }

        [Fact]
        public void CreateEqualExpression_DefaultStringExample_ReturnsExpectedExpression()
        {
            // arrange
            var collection = new[] { "abc", null, "def", "ghi" }.AsQueryable();

            // act
            var result = ExpressionHelper.CreateEqualExpression<string, string>(s => s, "def");

            // assert
            collection.Single(result).Should().Be("def");
        }

        [Fact]
        public void ChangeInputType_Default_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new T2 {Foo = "Foo"},
                new T2(),
                new T2 {Foo = "Bar"},
                new T2 {Foo = "Baz"},
            };
            Expression<Func<T1, bool>> filter = p => p.Foo == "Bar";

            // act
            var result = ExpressionHelper.ChangeInputType<T1, T2, bool>(filter);

            // assert
            collection.AsQueryable().Where(result).Should().ContainSingle(r => r.Foo == "Bar");
        }

        [Fact]
        public void SelectPropertyByName_ByName_ReturnsUsableExpression()
        {
            // arrange
            var data = new List<Model>
            {
                new Model {Id = 10, Content = "Hallo"},
                new Model {Id = 5, Content = "Welt"},
                new Model {Id = 3, Content = "Blup"},
                new Model {Id = 7, Content = "Bla"},
                new Model {Id = 13, Content = "Ende"},
            }.AsQueryable();

            // act
            var result = data.OrderBy(ExpressionHelper.SelectPropertyByName<Model, int>("Id"));

            // assert
            result.First().Id.Should().Be(3);
        }

        /// <summary>
        /// Wenn explizit eine ParameterExpression angegeben wird, sollte
        /// diese in der erzeugen Expression verwendet werden.
        /// </summary>
        [Fact]
        public void SelectPropertyByName_ExplicitParameter_ReturnsExpectedExpression()
        {
            // arrange
            var parameter = Expression.Parameter(typeof(Model), "x");
            var collection = new[]
            {
                new Model {Id = 12, Content = "MyContent"}
            };

            // act
            var result = ExpressionHelper.SelectPropertyByName<Model, string>("Content", parameter);

            // assert
            result.Should().NotBeNull("result");
            result.Parameters.Should().HaveCount(1, "result.Parameters.Count");
            parameter.Should().Be(result.Parameters.First());
            collection.AsQueryable().Select(result).Single().Should().Be("MyContent");
        }

        /// <summary>
        /// Wenn als explizit angegebene ParameterExpression null übergeben wird,
        /// sollte eine ArgumentNullException geworfen werden.
        /// </summary>
        [Fact]
        public void SelectPropertyByName_ExplicitParameterNull_ThrowsException()
        {
            // arrange
            Action fail = () => ExpressionHelper.SelectPropertyByName<Model, string>("Content", null);

            // act + assert
            fail.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Wenn eine explizit angegebene ParameterExpression einen falschen Typen aufweist,
        /// sollte eine InvalidOperationException geworfen werden.
        /// </summary>
        [Fact]
        public void SelectPropertyByName_ExplicitParameterTypeMismatch_ThrowsException()
        {
            // arrange
            var parameter = Expression.Parameter(typeof(object), "x");
            Action fail = () => ExpressionHelper.SelectPropertyByName<Model, string>("Content", parameter);

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SelectPropertyByNameX_ByName_ReturnsUsableExpression()
        {
            // arrange
            var data = new List<Model>
            {
                new Model {Id = 10, Content = "Hallo"},
                new Model {Id = 5, Content = "Welt"},
                new Model {Id = 3, Content = "Blup"},
                new Model {Id = 7, Content = "Bla"},
                new Model {Id = 13, Content = "Ende"},
            }.AsQueryable();

            // act
            var result = data.OrderBy((Expression<Func<Model, int>>)ExpressionHelper.SelectPropertyByNameX<Model>("Id"));

            // assert
            result.First().Id.Should().Be(3);
        }

        /// <summary>
        /// Wenn explizit eine ParameterExpression angegeben wird, sollte
        /// diese in der erzeugen Expression verwendet werden.
        /// </summary>
        [Fact]
        public void SelectPropertyByNameX_ExplicitParameter_ReturnsExpectedExpression()
        {
            // arrange
            var parameter = Expression.Parameter(typeof(Model), "x");
            var collection = new[]
            {
                new Model {Id = 12, Content = "MyContent"}
            };

            // act
            var result = ExpressionHelper.SelectPropertyByNameX<Model>("Content", parameter);

            // assert
            result.Should().NotBeNull("result");
            result.Parameters.Should().HaveCount(1, "result.Parameters.Count");
            parameter.Should().Be(result.Parameters.First());
            collection.AsQueryable().Select((Expression<Func<Model, string>>)result).Single().Should().Be("MyContent");
        }

        /// <summary>
        /// Wenn als explizit angegebene ParameterExpression null übergeben wird,
        /// sollte eine ArgumentNullException geworfen werden.
        /// </summary>
        [Fact]
        public void SelectPropertyByNameX_ExplicitParameterNull_ThrowsException()
        {
            // arrange
            Action fail = () => ExpressionHelper.SelectPropertyByNameX<Model>("Content", null);

            // act + assert
            fail.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Wenn eine explizit angegebene ParameterExpression einen falschen Typen aufweist,
        /// sollte eine InvalidOperationException geworfen werden.
        /// </summary>
        [Fact]
        public void SelectPropertyByNameX_ExplicitParameterTypeMismatch_ThrowsException()
        {
            // arrange
            var parameter = Expression.Parameter(typeof(object), "x");
            Action fail = () => ExpressionHelper.SelectPropertyByNameX<Model>("Content", parameter);

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ExtractMemberName_NoValidExpression_ReturnsNull()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass>(tc => "invalid");

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void ExtractMemberName_MemberExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass>(tc => tc.Member);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_UnaryExpressionDirectCast_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass>(tc => (float)tc.Member);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_BinaryExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass>(tc => tc.Member + 34);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_BinaryExpressionCompare_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass>(tc => 12 == tc.Member);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_BinaryExpressionMultipleMembers_ReturnsFirstName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass>(tc => tc.Member + tc.Member2);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_2_NoValidExpression_ReturnsNull()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass, string>(tc => "invalid");

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void ExtractMemberName_2_MemberExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass, int>(tc => tc.Member);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_2_BinaryExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass, int>(tc => tc.Member + 34);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_2_BinaryExpressionCompare_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass, bool>(tc => 12 == tc.Member);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberName_2_BinaryExpressionMultipleMembers_ReturnsFirstName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberName<TestClass, int>(tc => tc.Member + tc.Member2);

            // assert
            result.Should().Be("Member");
        }

        [Fact]
        public void ExtractMemberAttribute_NoValidExpression_ReturnsNull()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, CustomAttribute>(tc => "invalid");

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void ExtractMemberAttribute_MemberExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, CustomAttribute>(tc => tc.Member);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_UnaryExpressionDirectCast_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, CustomAttribute>(tc => (float)tc.Member);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_BinaryExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, CustomAttribute>(tc => tc.Member + 34);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_BinaryExpressionCompare_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, CustomAttribute>(tc => 12 == tc.Member);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_BinaryExpressionMultipleMembers_ReturnsFirstName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, CustomAttribute>(tc => tc.Member + tc.Member2);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_2_NoValidExpression_ReturnsNull()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, string, CustomAttribute>(tc => "invalid");

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void ExtractMemberAttribute_2_MemberExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, int, CustomAttribute>(tc => tc.Member);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_2_BinaryExpression_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, int, CustomAttribute>(tc => tc.Member + 34);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_2_BinaryExpressionCompare_ReturnsName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, bool, CustomAttribute>(tc => 12 == tc.Member);

            // assert
            result.Name.Should().Be("myMember1");
        }

        [Fact]
        public void ExtractMemberAttribute_2_BinaryExpressionMultipleMembers_ReturnsFirstName()
        {
            // act
            var result = ExpressionHelper.ExtractMemberAttribute<TestClass, int, CustomAttribute>(tc => tc.Member + tc.Member2);

            // assert
            result.Name.Should().Be("myMember1");
        }

        private class TestClass : TestClassBase
        {
            [CustomAttribute("myMember1")]
            public override int Member { get; set; }

            [CustomAttribute("myMember2")]
            public override int Member2 { get; set; }
        }

        private class TestClassBase
        {
            [CustomAttribute("myMember1Base")]
            public virtual int Member { get; set; }

            [CustomAttribute("myMember2Base")]
            public virtual int Member2 { get; set; }
        }

        private class Model
        {
            public int Id { get; set; }
            public string Content { get; set; }
        }

        private class T2 : T1
        {
        }

        private class T1
        {
            public string Foo { get; set; }
        }

        private class CustomAttribute : Attribute
        {
            public string Name { get; }

            public CustomAttribute(string name)
            {
                Name = name;
            }
        }
    }
}