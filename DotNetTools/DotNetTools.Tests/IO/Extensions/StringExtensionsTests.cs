using System.IO;
using System.Text;
using Dataport.AppFrameDotNet.DotNetTools.IO.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.IO.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void AsStream_Default_ReturnsValidStreamRepresentation()
        {
            // arrange
            var str = "Hallo Welt";

            // act
            using (var result = str.AsStream(Encoding.UTF8))
            // assert
            using (var reader = new StreamReader(result, Encoding.UTF8))
            {
                result.Position = 0;
                var converted = reader.ReadToEnd();
                converted.Should().Be(str);
            }
        }
    }
}