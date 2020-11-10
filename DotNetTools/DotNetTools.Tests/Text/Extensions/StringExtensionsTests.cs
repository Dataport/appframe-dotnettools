using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Dataport.AppFrameDotNet.DotNetTools.Text.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Text.Extensions
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class StringExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ToBase64Provider))]
        public void ToBase64_WithCases_ReturnsExpectedBase64(string input, Encoding encoding, string expected)
        {
            // act
            var result = input.ToBase64(encoding);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(FromBase64Provider))]
        public void FromBase64_WithCases_ReturnsExpectedBase64(string input, Encoding encoding, string expected)
        {
            // act
            var result = input.FromBase64(encoding);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, 2, null)]
        [InlineData("", 2, "")]
        [InlineData("abcde", 0, "")]
        [InlineData("abcde", 3, "abc")]
        [InlineData("abcde", 5, "abcde")]
        [InlineData("abcde", 7, "abcde")]
        public void Truncate_Cases_ReturnsExpectedResult(string input, int maxLength, string expectedResult)
        {
            // act
            var result = input.Truncate(maxLength);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Truncate_InvalidLength_ThrowsException()
        {
            // arrange
            var input = "abcde";

            Action fail = () => input.Truncate(-1);

            // act + assert
            fail.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("B", false)]
        [InlineData("B", true)]
        [InlineData("b", true)]
        [InlineData("1", false)]
        [InlineData("1", true)]
        public void ToEnumMember_Cases_Works(string input, bool ignoreCase)
        {
            // act
            var result = input.ToEnumMember<TestEnum>(ignoreCase);

            // assert
            result.Should().Be(TestEnum.B);
        }

        [Theory]
        [InlineData("b", false)]
        [InlineData("-1", true)]
        [InlineData("3", true)]
        [InlineData("D", false)]
        [InlineData("D", true)]
        public void ToEnumMember_InvalidInput_Throws(string input, bool ignoreCase)
        {
            // arrange
            Action fail = () => input.ToEnumMember<TestEnum>(ignoreCase);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("B", false, true, TestEnum.B)]
        [InlineData("B", true, true, TestEnum.B)]
        [InlineData("b", true, true, TestEnum.B)]
        [InlineData("1", false, true, TestEnum.B)]
        [InlineData("1", true, true, TestEnum.B)]
        [InlineData("b", false, false, default(TestEnum))]
        [InlineData("-1", true, false, default(TestEnum))]
        [InlineData("3", true, false, default(TestEnum))]
        [InlineData("D", false, false, default(TestEnum))]
        [InlineData("D", true, false, default(TestEnum))]
        public void TryToEnumMember_Cases_ReturnsExpectedResult(string input, bool ignoreCase, bool expectedResult, object expectedMember)
        {
            // act
            var result = input.TryToEnumMember(ignoreCase, out TestEnum member);

            // assert
            result.Should().Be(expectedResult);
            member.Should().Be(expectedMember);
        }

        [Fact]
        public void EscapeXml_StringWithXmlElements_EscapesCorrect()
        {
            // arrange
            var str = "<cookie page=\"1\"><accountid last=\"{E062B974-7F8D-DC11-9048-0003FF27AC3B}\" name=\"Ben & Jerries\"/></cookie>";
            var expected = "&lt;cookie page=&quot;1&quot;&gt;&lt;accountid last=&quot;{E062B974-7F8D-DC11-9048-0003FF27AC3B}&quot; name=&quot;Ben &amp; Jerries&quot;/&gt;&lt;/cookie&gt;";

            // act
            var result = str.EscapeXml();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void EmptyAsNull_WithEmptyCases_ReturnsNull(string input)
        {
            // act
            var result = input.EmptyAsNull();

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void EmptyAsNull_NotEmpty_ReturnsInitialValue()
        {
            // arrange
            var value = "test";

            // act
            var result = value.EmptyAsNull();

            // assert
            result.Should().Be(value);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "   ")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("haus", "Haus")]
        [InlineData("Haus", "Haus")]
        [InlineData("mein schönes Haus", "Mein schönes Haus")]
        [InlineData("Mein schönes Haus", "Mein schönes Haus")]
        public void FirstLetterUpperCase(string input, string expected)
        {
            // act
            var result = input.FirstLetterUpperCase();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, null, "foo", null, "bar", "")]
        [InlineData("", "")]
        [InlineData("", "", "foo", null, "bar", "")]
        [InlineData("Batman", "Batman")]
        [InlineData("Batman", "Batman", null, "")]
        [InlineData("Batman", "Bmn", "a", null, "t", "")]
        public void Strip_Cases_ReturnsExpectedResult(string input, string expected, params string[] toStrip)
        {
            // act
            var result = input.Strip(toStrip);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, null, false, null)]
        [InlineData(null, "", false, null)]
        [InlineData("", "asd", false, "")]
        [InlineData("Hallo", null, false, "Hallo")]
        [InlineData("Hallo", "", false, "Hallo")]
        [InlineData("Hallo", "l", false, "Hallo")]
        [InlineData("an anna", "an", false, "an anna")]
        [InlineData("an anna", "an", true, " an")]
        [InlineData("ich ja ich", "ich", false, " ja ")]
        [InlineData("[test]", "[", false, "[test]")]
        [InlineData("[test]", "[", true, "test")]
        public void StripSurrounding_Cases_ReturnsExpectedResult(string input, string surrounding, bool inverted, string expected)
        {
            // act
            var result = input.StripSurrounding(surrounding, inverted);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("hallo test", "hallo test")]
        [InlineData("hallo\r\ntest", "hallo test")]
        [InlineData("hallo\r\n\r\ntest", "hallo test")]
        [InlineData("hallo\r\n\r\n\r\ntest", "hallo test")]
        [InlineData("hallo\ntest", "hallo test")]
        [InlineData("hallo\rtest", "hallo test")]
        [InlineData("hallo     test", "hallo test")]
        public void StripLineFeeds_Cases_ReturnsExpectedResult(string input, string expected)
        {
            // act
            var result = input.StripLineFeeds();

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void CleanJson_OldUseCase_ReturnsExpectedResult()
        {
            var expected = "Hal\r\nlo";
            var result = "\"Hal\\r\\nlo\"".CleanJson();

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "   ")]
        [InlineData("foo", "oof")]
        [InlineData("{test}", "{tset}")]
        [InlineData("[test]", "[tset]")]
        [InlineData("<test>", "<tset>")]
        [InlineData("(test)", "(tset)")]
        [InlineData("\\test/", "\\tset/")]
        [InlineData("}test{", "}tset{")]
        [InlineData("]test[", "]tset[")]
        [InlineData(">test<", ">tset<")]
        [InlineData(")test(", ")tset(")]
        [InlineData("/test\\", "/tset\\")]
        [InlineData("a > b", "b < a")]
        [InlineData("a < b", "b > a")]
        [InlineData("a |%| b", "b |%| a")]
        public void ReverseLogical_Cases_ReturnsExpectedResult(string input, string expected)
        {
            // act
            var result = input.ReverseLogical();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, "a", "b", null)]
        [InlineData("", "a", "b", "")]
        [InlineData("foobar", "x", "y", "foobar")]
        [InlineData("Haus", "au", "as", "Hass")]
        [InlineData("nahhhhhh", "ah", "a", "na")]
        public void ReplaceRecursive_Cases_ReturnsExpectedResult(string input, string oldValue, string newValue, string expected)
        {
            // act
            var result = input.ReplaceRecursive(oldValue, newValue);

            // assert
            result.Should().Be(expected);
        }

        private enum TestEnum
        {
            A,
            B,
            C
        }

        private class ToBase64Provider : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "Horton hört ein hää", Encoding.ASCII, "SG9ydG9uIGg/cnQgZWluIGg/Pw==" };
                yield return new object[] { "Horton hört ein hää", Encoding.UTF8, "SG9ydG9uIGjDtnJ0IGVpbiBow6TDpA==" };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class FromBase64Provider : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "SG9ydG9uIGg/cnQgZWluIGg/Pw==", Encoding.ASCII, "Horton h?rt ein h??" };
                yield return new object[] { "SG9ydG9uIGjDtnJ0IGVpbiBow6TDpA==", Encoding.UTF8, "Horton hört ein hää" };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}