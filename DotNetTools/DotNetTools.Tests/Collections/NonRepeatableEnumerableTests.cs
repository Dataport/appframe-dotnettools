using Dataport.AppFrameDotNet.DotNetTools.Collections;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections
{
    public class NonRepeatableEnumerableTests
    {
        [Fact]
        public void GetEnumerator_EnumerateFull_EnumeratesOnlyOnce()
        {
            // arrange
            var enumerable = new NonRepeatableEnumerable<int>(Enumerable.Range(1, 5));

            // act
            var list = enumerable.ToList();

            // assert
            list.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
            enumerable.Should().BeEmpty();
        }

        [Fact]
        public void GetEnumerator_EnumerateParital_EnumeratesInParts()
        {
            // arrange
            var enumerable = new NonRepeatableEnumerable<int>(Enumerable.Range(1, 5));

            // act
            var list = enumerable.Take(3).ToList();

            // assert
            list.Should().BeEquivalentTo(new[] { 1, 2, 3 });
            enumerable.Should().BeEquivalentTo(new[] { 4, 5 });
        }
    }
}