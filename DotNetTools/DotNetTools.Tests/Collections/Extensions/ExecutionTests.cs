using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    public class ExecutionTests
    {
        [Fact]
        public void ForEach_WithValues_Executes()
        {
            // arrange
            var numbers = new[] { 1, 2, 3 };
            var result = 0;

            // act
            numbers.ForEach(n => result += n);

            // assert
            result.Should().Be(6);
        }
    }
}