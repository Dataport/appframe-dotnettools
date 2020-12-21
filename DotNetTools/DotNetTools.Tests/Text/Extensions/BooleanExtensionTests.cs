using Dataport.AppFrameDotNet.DotNetTools.Text.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Text.Extensions
{
    public class BooleanExtensionTests
    {
        [Theory]
        [InlineData(null, "No")]
        [InlineData(null, "Neee", "Ja", "Neee")]
        [InlineData(false, "No")]
        [InlineData(false, "Neee", "Ja", "Neee")]
        [InlineData(true, "Yes")]
        [InlineData(true, "Ja", "Ja", "Neee")]
        public void ToYesNo_NullableBool_ReturnsExpectedResult(bool? value, string expected, string yes = "Yes", string no = "No")
        {
            // act
            var result = value.ToYesNo(yes, no);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(false, "No")]
        [InlineData(false, "Neee", "Ja", "Neee")]
        [InlineData(true, "Yes")]
        [InlineData(true, "Ja", "Ja", "Neee")]
        public void ToYesNo_Bool_ReturnsExpectedResult(bool value, string expected, string yes = "Yes", string no = "No")
        {
            // act
            var result = value.ToYesNo(yes, no);

            // assert
            result.Should().Be(expected);
        }
    }
}