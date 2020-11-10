using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Dataport.AppFrameDotNet.DotNetTools.Comparison.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Comparison.Extensions
{
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void IsEquivalentTo_BothNull_ReturnsTrue()
        {
            // arrange
            Dictionary<int, string> dict = null;

            // act
            var result = dict.IsEquivalentTo(null);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEquivalentTo_LeftNull_ReturnsFalse()
        {
            // arrange
            Dictionary<int, string> dict = null;

            // act
            var result = dict.IsEquivalentTo(new Dictionary<int, string>());

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEquivalentTo_RightNull_ReturnsFalse()
        {
            // arrange
            var dict = new Dictionary<int, string>();

            // act
            var result = dict.IsEquivalentTo(null);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEquivalentTo_Equals_ReturnsTrue()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = a.IsEquivalentTo(b);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEquivalentTo_EqualsDifferentOrder_ReturnsTrue()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {3, "C"},
                {1, "A"},
                {2, "B"},
            };

            // act
            var result = a.IsEquivalentTo(b);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEquivalentTo_Differs_ReturnsFalse()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {4, "D"},
            };

            // act
            var result = a.IsEquivalentTo(b);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEquivalentTo_LeftMore_ReturnsFalse()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
                {4, "D"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = a.IsEquivalentTo(b);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEquivalentTo_RightMore_ReturnsFalse()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
                {4, "D"},
            };

            // act
            var result = a.IsEquivalentTo(b);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubsetOf_BothNull_ReturnsTrue()
        {
            // arrange
            Dictionary<int, string> dict = null;

            // act
            var result = dict.IsSubsetOf(null);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSubsetOf_LeftNull_ReturnsFalse()
        {
            // arrange
            Dictionary<int, string> dict = null;

            // act
            var result = dict.IsSubsetOf(new Dictionary<int, string>());

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubsetOf_RightNull_ReturnsFalse()
        {
            // arrange
            var dict = new Dictionary<int, string>();

            // act
            var result = dict.IsSubsetOf(null);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubsetOf_Equals_ReturnsTrue()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = a.IsSubsetOf(b);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSubsetOf_EqualsDifferentOrder_ReturnsTrue()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {3, "C"},
                {1, "A"},
                {2, "B"},
            };

            // act
            var result = a.IsSubsetOf(b);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSubsetOf_Differs_ReturnsFalse()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {4, "D"},
            };

            // act
            var result = a.IsSubsetOf(b);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubsetOf_LeftMore_ReturnsFalse()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
                {4, "D"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };

            // act
            var result = a.IsSubsetOf(b);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubsetOf_RightMore_ReturnsTrue()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
                {4, "D"},
            };

            // act
            var result = a.IsSubsetOf(b);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSubsetOf_RightMoreDifferentOrder_ReturnsTrue()
        {
            // arrange
            var a = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"},
                {3, "C"},
            };
            var b = new Dictionary<int, string>
            {
                {4, "D"},
                {2, "B"},
                {1, "A"},
                {3, "C"},
            };

            // act
            var result = a.IsSubsetOf(b);

            // assert
            result.Should().BeTrue();
        }
    }
}