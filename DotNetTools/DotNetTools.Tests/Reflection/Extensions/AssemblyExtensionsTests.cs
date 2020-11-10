using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using System;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    public class AssemblyExtensionsTests
    {
        [Fact]
        public void LoadEmbeddedResource_WithFilename_ReturnsFileContent()
        {
            // arrange
            var assembly = typeof(AssemblyExtensionsTests).Assembly;
            var filename = "TestResource.txt";

            // act
            var result = assembly.LoadEmbeddedResource(filename);

            // assert
            result.Should().Be("Das ist ein Test");
        }

        [Fact]
        public void LoadEmbeddedResource_WithFilepath_ReturnsFileContent()
        {
            // arrange
            var assembly = typeof(AssemblyExtensionsTests).Assembly;
            var filePath = "Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Helpers.TestResource.txt";

            // act
            var result = assembly.LoadEmbeddedResource(filePath);

            // assert
            result.Should().Be("Das ist ein Test");
        }

        [Fact]
        public void LoadEmbeddedResource_FileNotExists_ThrowsException()
        {
            // arrange
            var assembly = typeof(AssemblyExtensions).Assembly;
            var filename = "FooBar.txt";

            Action fail = () => assembly.LoadEmbeddedResource(filename);

            // act + assert
            fail.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void LoadEmbeddedResource_FileExistsTwiceAndOnlyNameGiven_ThrowsException()
        {
            // arrange
            var assembly = typeof(AssemblyExtensions).Assembly;
            var filename = "ResourceA.txt";

            Action fail = () => assembly.LoadEmbeddedResource(filename);

            // act + assert
            fail.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void LoadEmbeddedResource_FileExistsTwiceAndPathGiven_ReturnsFileContent()
        {
            // arrange
            var assembly = typeof(AssemblyExtensionsTests).Assembly;
            var filePath1 = "Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Helpers.ResourceA.txt";
            var filePath2 = "Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Helpers.Sub.ResourceA.txt";

            // act
            var result1 = assembly.LoadEmbeddedResource(filePath1);
            var result2 = assembly.LoadEmbeddedResource(filePath2);

            // assert
            result1.Should().Be("Doppelte Resource (Not Sub)");
            result2.Should().Be("Doppelte Resource (Sub)");
        }

        [Fact]
        public void LoadEmbeddedResource_FileNotResource_ThrowsException()
        {
            // arrange
            var assembly = typeof(AssemblyExtensions).Assembly;
            var filename = "AssemblyExtensionsTests.cs";

            Action fail = () => assembly.LoadEmbeddedResource(filename);

            // act + assert
            fail.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}