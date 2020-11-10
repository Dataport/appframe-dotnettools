using Dataport.AppFrameDotNet.DotNetTools.Numeric.Extensions;
using FluentAssertions;
using System;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Numeric.Extensions
{
    public class FormatingTests
    {
        [Theory]
        [InlineData(0, 3, "000")]
        [InlineData(5, 1, "5")]
        [InlineData(2, 2, "02")]
        [InlineData(18, 5, "00018")]
        [InlineData(-2, 5, "-00002")]
        [InlineData(-20, 2, "-20")]
        public void WithLeadingZeros_Cases_ReturnsExpectedResult(int i, int leadingZeros, string expected)
        {
            // act
            var result = i.WithLeadingZeros(leadingZeros);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, -4)]
        [InlineData(1, 0)]
        [InlineData(18, 1)]
        [InlineData(-20, 1)]
        public void WithLeadingZeros_ReferenceToLow_ThrowsException(int i, int reference)
        {
            // arrange
            Action fail = () => i.WithLeadingZeros(reference);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(-15, "Minus fünfzehn")]
        [InlineData(0, "Null")]
        [InlineData(16, "Sechzehn")]
        [InlineData(22, "Zweiundzwanzig")]
        [InlineData(157, "Einhundertsiebenundfünfzig")]
        [InlineData(10572, "Zehntausendfünfhundertzweiundsiebzig")]
        public void AsWords_ShortCases_ReturnsExpectedResult(short i, string expected)
        {
            // act
            var result = i.AsWords();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-15, "Minus fünfzehn")]
        [InlineData(0, "Null")]
        [InlineData(16, "Sechzehn")]
        [InlineData(22, "Zweiundzwanzig")]
        [InlineData(157, "Einhundertsiebenundfünfzig")]
        [InlineData(10572, "Zehntausendfünfhundertzweiundsiebzig")]
        [InlineData(33000, "Dreiunddreißigtausend")]
        [InlineData(222318, "Zweihundertzweiundzwanzigtausenddreihundertachtzehn")]
        [InlineData(1000000, "Einemillion")]
        [InlineData(2000000, "Zweimillionen")]
        [InlineData(10400120, "Zehnmillionenvierhunderttausendeinhundertzwanzig")]
        [InlineData(1000000000, "Einemilliarde")]
        [InlineData(2000000014, "Zweimilliardenvierzehn")]
        public void AsWords_IntCases_ReturnsExpectedResult(int i, string expected)
        {
            // act
            var result = i.AsWords();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-15, "Minus fünfzehn")]
        [InlineData(0, "Null")]
        [InlineData(16, "Sechzehn")]
        [InlineData(22, "Zweiundzwanzig")]
        [InlineData(157, "Einhundertsiebenundfünfzig")]
        [InlineData(10572, "Zehntausendfünfhundertzweiundsiebzig")]
        [InlineData(33000, "Dreiunddreißigtausend")]
        [InlineData(222318, "Zweihundertzweiundzwanzigtausenddreihundertachtzehn")]
        [InlineData(1000000, "Einemillion")]
        [InlineData(2000000, "Zweimillionen")]
        [InlineData(10400120, "Zehnmillionenvierhunderttausendeinhundertzwanzig")]
        [InlineData(1000000000, "Einemilliarde")]
        [InlineData(2000000014, "Zweimilliardenvierzehn")]
        [InlineData(9223372036854775807, "Neuntrillionenzweihundertdreiundzwanzigbilliardendreihundertzweiundsiebzigbillionensechsunddreißigmilliardenachthundertvierundfünfzigmillionensiebenhundertfünfundsiebzigtausendachthundertsieben")]
        public void AsWords_LongCases_ReturnsExpectedResult(long i, string expected)
        {
            // act
            var result = i.AsWords();

            // assert
            result.Should().Be(expected);
        }
    }
}