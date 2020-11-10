using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public class InstanceExtensionsTests
    {
        [Fact]
        public void Get_NoInstance_ThrowsException()
        {
            // arrange
            TestClass c = null;

            Action fail = () => c.Get(cl => cl.Member);

            // act + assert
            fail.Should().Throw<NullReferenceException>().Which.Message.Should().Contain("TestClass:");
        }

        [Fact]
        public void Get_WithInstance_ReturnsValue()
        {
            // arrange
            var tc = new TestClass();
            tc.Member = 23;

            // act
            var result = tc.Get(c => c.Member);

            // assert
            result.Should().Be(23);
        }

        [Fact]
        public void Set_NoInstance_ThrowsException()
        {
            // arrange
            TestClass c = null;

            Action fail = () => c.Set(cl => cl.Member = 12);

            // act + assert
            fail.Should().Throw<NullReferenceException>().Which.Message.Should().Contain("TestClass:");
        }

        [Fact]
        public void Set_WithInstance_ReturnsValue()
        {
            // arrange
            var tc = new TestClass();
            tc.Member = 23;

            // act
            tc.Set(c => c.Member = 34);

            // assert
            tc.Member.Should().Be(34);
        }

        [Fact]
        public void ObjectComparsionExtensions_MergeWith()
        {
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = null, Nummer = 0, Egal = "Zwei" };

            target.MergeWith(source);

            target.Text.Should().Be("Hallo");
            target.Nummer.Should().Be(1);
            target.Egal.Should().Be("Zwei");
        }

        private class TestClass
        {
            public int Member { get; set; }
        }

        private class Testklasse
        {
            public string Text { get; set; }

            public int Nummer { get; set; }

            public string Egal { get; set; }
        }
    }
}