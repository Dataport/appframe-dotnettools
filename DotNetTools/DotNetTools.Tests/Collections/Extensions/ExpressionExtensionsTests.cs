using System;
using System.Linq;
using System.Linq.Expressions;
using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    public class ExpressionExtensionsTests
    {
        [Fact]
        public void ChangeInputType_Default_ReturnsExpectedResult()
        {
            // arrange
            var collection = new[]
            {
                new T2 {Foo = "Foo"},
                new T2 {Foo = "Bar"},
                new T2 {Foo = "Baz"},
            };
            Expression<Func<T1, bool>> filter = p => p.Foo == "Bar";

            // act
            var result = filter.ChangeInputType<T1, T2, bool>();

            // assert
            collection.AsQueryable().Where(result).Should().ContainSingle(r => r.Foo == "Bar");
        }

        private class T2 : T1
        {
        }

        private class T1
        {
            public string Foo { get; set; }
        }
    }
}