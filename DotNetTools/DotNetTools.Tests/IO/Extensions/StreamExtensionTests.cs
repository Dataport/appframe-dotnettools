using System.IO;
using System.Text;
using Dataport.AppFrameDotNet.DotNetTools.IO.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.IO.Extensions
{
    public class StreamExtensionTests
    {
        [Fact]
        public void ReadAllBytes_MemoryStream_ReturnsExpectedResult()
        {
            // arrange
            var value = "Hallo Welt";
            byte[] result;

            // act
            using (var valueStream = value.AsStream(Encoding.UTF8))
            {
                valueStream.Position = 10;
                result = valueStream.ReadAllBytes();
            }

            // assert
            Encoding.UTF8.GetString(result).Should().Be(value);
        }

        [Fact]
        public void ReadAllBytes_NotMemoryStream_ReturnsExpectedResult()
        {
            // arrange
            var value = "Hallo Welt";
            byte[] result;

            // act
            using (var valueStream = value.AsStream(Encoding.UTF8))
            using (var stream = new BufferedStream(valueStream))
            {
                stream.Position = 10;
                result = valueStream.ReadAllBytes();
            }

            // assert
            Encoding.UTF8.GetString(result).Should().Be(value);
        }
    }
}