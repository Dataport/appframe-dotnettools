using Dataport.AppFrameDotNet.DotNetTools.Collections;
using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class TransformationTests
    {
        [Fact]
        public void ToDictionary_EmptyGrouping_ShouldBuildCorrectly()
        {
            // arrange
            IEnumerable<IGrouping<string, User>> group = new List<User>().GroupBy(x => x.FirstName);

            // act
            var dict = group.ToDictionary();

            // assert
            dict.Should().BeEmpty();
        }

        [Fact]
        public void ToDictionary_FilledGrouping_ShouldBuildCorrectly()
        {
            // arrange
            User johnDoe = new User { FirstName = "John", LastName = "Doe" };
            User robinsonCrusoe = new User { FirstName = "Robinson", LastName = "Crusoe" };
            User johnBonJovi = new User { FirstName = "John", LastName = "Bon Jovi" };
            IEnumerable<IGrouping<string, User>> group = new[] { johnDoe, robinsonCrusoe, johnBonJovi }.GroupBy(x => x.FirstName);

            // act
            var dict = group.ToDictionary();

            // assert
            dict.Should().HaveCount(2);
            dict.Should().ContainKey("John");
            dict["John"].Should().HaveCount(2);
            dict["John"].Should().Contain(new[] { johnDoe, johnBonJovi });
            dict.Should().ContainKey("Robinson");
            dict["Robinson"].Should().HaveCount(1);
            dict["Robinson"].Should().Contain(robinsonCrusoe);
        }

        [Fact]
        public void CrossJoin_AsTuple_ShouldWork()
        {
            // arrange
            var left = new List<int> { 1, 2, 3 };
            var right = new List<string> { "a", "b" };
            var expected = new[]
            {
                new Tuple<int, string>(1, "a"),
                new Tuple<int, string>(1, "b"),
                new Tuple<int, string>(2, "a"),
                new Tuple<int, string>(2, "b"),
                new Tuple<int, string>(3, "a"),
                new Tuple<int, string>(3, "b"),
            };

            // act
            var result = left.CrossJoin(right);

            // assert
            result.Should().BeEquivalentTo(expected.AsEnumerable());
        }

        [Fact]
        public void CrossJoin_WithTransformation_ShouldWork()
        {
            // arrange
            var left = new List<int> { 1, 2, 3 };
            var right = new List<string> { "a", "b" };
            var expected = new[] { "1a", "1b", "2a", "2b", "3a", "3b" };

            // act
            var result = left.CrossJoin(right, (l, r) => $"{l}{r}");

            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        public void Chunk_ChunkSizeInvalid_ShouldFail(int chunksize)
        {
            // arrange
            var values = Enumerable.Range(1, 10);

            Action fail = () => values.Chunk(chunksize).ToList();

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Chunk_Empty_ShouldWork()
        {
            // arrange
            var values = Enumerable.Empty<int>();

            // act
            var result = values.Chunk(3).ToList();

            // assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public void Chunk_WithRangeFitting_ShouldWork()
        {
            // arrange
            var values = Enumerable.Range(1, 10);

            // act
            var result = values.Chunk(5).ToList();

            // assert
            result.Should().HaveCount(2);
            result.First().Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
            result.Second().Should().BeEquivalentTo(new[] { 6, 7, 8, 9, 10 });
        }

        [Fact]
        public void Chunk_WithoutRangeFitting_ShouldWork()
        {
            // arrange
            var values = Enumerable.Range(1, 10);

            // act
            var result = values.Chunk(4).ToList();

            // assert
            result.Should().HaveCount(3);
            result.First().Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
            result.Second().Should().BeEquivalentTo(new[] { 5, 6, 7, 8 });
            result.Third().Should().BeEquivalentTo(new[] { 9, 10 });
        }

        [Fact]
        public void Chunk_WithPartialConsuming_ShouldWork()
        {
            // arrange
            var values = new NonRepeatableEnumerable<int>(Enumerable.Range(1, 10));

            // act
            var result = values.Chunk(4).Take(2).ToList();

            // assert
            result.Should().HaveCount(2);
            result.First().Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
            result.Second().Should().BeEquivalentTo(new[] { 5, 6, 7, 8 });
            values.Should().BeEquivalentTo(new[] { 9, 10 });
        }

        [Fact]
        public void Join_WithCollectionNull_ReturnsEmptyString()
        {
            // arrange
            IEnumerable<string> collection = null;

            // act
            var result = collection.Join(",");

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Join_WithSeparatorNull_BehavesLikeEmptySeparator()
        {
            // arrange
            var collection = new[] { "abc", "def" };

            // act
            var result = collection.Join(null);

            // assert
            result.Should().Be("abcdef");
        }

        [Fact]
        public void Join_WithString_ReturnsExpectedResult()
        {
            // arrange
            var str = "abc";
            var separator = ",";

            // act
            var result = str.Join(separator);

            // assert
            result.Should().Be("a,b,c");
        }

        [Fact]
        public void Join_WithInt_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 1, 2, 3 };

            // act
            var result = collection.Join(";");

            // assert
            result.Should().Be("1;2;3");
        }

        [Fact]
        public void Join_WithEmptyElements_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { "abc", null, "def", string.Empty, " ", "ghi" };

            // act
            var result = collection.Join(",");

            // assert
            result.Should().Be("abc,,def,, ,ghi");
        }

        [Fact]
        public void Join_WithCustomToString_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new User { FirstName = "Hans", LastName = "Franz" },
                new User { FirstName = "Barry", LastName = "Wild" },
            };

            // act
            var result = collection.Join(",");

            // assert
            result.Should().Be("H. Franz,B. Wild");
        }

        [Fact]
        public void Join_WithNestedCollections_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new[] { 1, 2, 3 },
                null,
                Array.Empty<int>(),
                new[] { 4, 5, 6 },
            };

            // act
            var result = collection.Join(",");

            // assert
            result.Should().Be("System.Int32[],,System.Int32[],System.Int32[]");
        }

        [Fact]
        public void JoinNotEmpty_WithCollectionNull_ReturnsEmptyString()
        {
            // arrange
            IEnumerable<string> collection = null;

            // act
            var result = collection.JoinNotEmpty(",");

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void JoinNotEmpty_WithSeparatorNull_BehavesLikeEmptySeparator()
        {
            // arrange
            var collection = new[] { "abc", "def" };

            // act
            var result = collection.JoinNotEmpty(null);

            // assert
            result.Should().Be("abcdef");
        }

        [Fact]
        public void JoinNotEmpty_WithString_ReturnsExpectedResult()
        {
            // arrange
            var str = "abc";
            var separator = ",";

            // act
            var result = str.JoinNotEmpty(separator);

            // assert
            result.Should().Be("a,b,c");
        }

        [Fact]
        public void JoinNotEmpty_WithInt_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { 1, 2, 3 };

            // act
            var result = collection.JoinNotEmpty(";");

            // assert
            result.Should().Be("1;2;3");
        }

        [Fact]
        public void JoinNotEmpty_WithEmptyElements_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[] { "abc", null, "def", string.Empty, " ", "ghi" };

            // act
            var result = collection.JoinNotEmpty(",");

            // assert
            result.Should().Be("abc,def,ghi");
        }

        [Fact]
        public void JoinNotEmpty_WithCustomToString_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new User { FirstName = "Hans", LastName = "Franz" },
                new User { FirstName = "Barry", LastName = "Wild" },
            };

            // act
            var result = collection.JoinNotEmpty(",");

            // assert
            result.Should().Be("H. Franz,B. Wild");
        }

        [Fact]
        public void JoinNotEmpty_WithNestedCollections_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new[] { 1, 2, 3 },
                null,
                Array.Empty<int>(),
                new[] { 4, 5, 6 },
            };

            // act
            var result = collection.JoinNotEmpty(",");

            // assert
            result.Should().Be("System.Int32[],System.Int32[],System.Int32[]");
        }

        [Fact]
        public void PagingExtensionsTests_Page()
        {
            var data = new List<Model>
            {
                new Model {Id = 10, Content = "Hallo"},
                new Model {Id = 5, Content = "Welt"},
                new Model {Id = 3, Content = "Blup"},
                new Model {Id = 7, Content = "Bla"},
                new Model {Id = 13, Content = "Ende"},
            }.OrderBy(x => x.Id);

            var page0 = data.Page(2, 0);

            page0.Items.Should().HaveCount(2, "page0: Items.Count");
            page0.Items.First().Id.Should().Be(3, "page0: Items.First().Id");
            page0.Items.Last().Id.Should().Be(5, "page0: Items.Last().Id");
            page0.PageCount.Should().Be(3, "page0: PageCount");
            page0.TotalItemCount.Should().Be(5, "page0: TotalItemCount");
            page0.CurrentPage.Should().Be(0, "page0: CurrentPage");

            var page1 = data.Page(2, 1);

            page1.Items.Should().HaveCount(2, "page1: Items.Count");
            page1.Items.First().Id.Should().Be(7, "page1: Items.First().Id");
            page1.Items.Last().Id.Should().Be(10, "page1: Items.Last().Id");
            page1.PageCount.Should().Be(3, "page1: PageCount");
            page1.TotalItemCount.Should().Be(5, "page1: TotalItemCount");
            page1.CurrentPage.Should().Be(1, "page1: CurrentPage");

            var page2 = data.Page(2, 2);

            page2.Items.Should().HaveCount(1, "page2: Items.Count");
            page2.Items.First().Id.Should().Be(13, "page2: Items.First().Id");
            page2.PageCount.Should().Be(3, "page2: PageCount");
            page2.TotalItemCount.Should().Be(5, "page2: TotalItemCount");
            page2.CurrentPage.Should().Be(2, "page2: CurrentPage");
        }

        [Fact]
        public void PagingExtensionsTests_Page_ZeroItems()
        {
            var data = new List<Model>().AsQueryable().OrderBy(x => x.Id);

            var page0 = data.Page(2, 0);

            page0.Items.Should().HaveCount(0, "page0: Items.Count");
            page0.PageCount.Should().Be(0, "page0: PageCount");
            page0.TotalItemCount.Should().Be(0, "page0: TotalItemCount");
            page0.CurrentPage.Should().Be(-1, "page0: CurrentPage");
        }

        [Fact]
        public void PagingExtensionsTests_Page_RequestedPageOutOfRange()
        {
            var data = new List<Model>
            {
                new Model {Id = 10, Content = "Hallo"},
                new Model {Id = 5, Content = "Welt"},
                new Model {Id = 3, Content = "Blup"},
                new Model {Id = 7, Content = "Bla"},
                new Model {Id = 13, Content = "Ende"},
            }.OrderBy(x => x.Id);

            var page0 = data.Page(2, 7);

            page0.Items.Should().HaveCount(0, "page0: Items.Count");
            page0.PageCount.Should().Be(3, "page0: PageCount");
            page0.TotalItemCount.Should().Be(5, "page0: TotalItemCount");
            page0.CurrentPage.Should().Be(-1, "page0: CurrentPage");
        }

        [Fact]
        public void PagingExtensionsTests_Page_NegativeRequestedPage()
        {
            var data = new List<Model>().AsQueryable().OrderBy(x => x.Id);

            Action fail = () => data.Page(2, -1);

            fail.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PagingExtensionsTests_Page_NotPlausiblePageSite()
        {
            var data = new List<Model>().AsQueryable().OrderBy(x => x.Id);

            Action fail = () => data.Page(0, 1);

            fail.Should().Throw<ArgumentOutOfRangeException>();
        }

        private class Model
        {
            public int Id { get; set; }
            public string Content { get; set; }
        }

        private class User
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public override string ToString()
            {
                return FirstName[0] + ". " + LastName;
            }
        }
    }
}