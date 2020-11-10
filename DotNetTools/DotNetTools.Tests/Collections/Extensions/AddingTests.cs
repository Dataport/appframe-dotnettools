using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;
using FluentAssertions;
using Moq;
using Xunit;

// TODO Version 4: Pragma entfernen, dieses dient nur für die obsolete AddIfMissing-Methode
#pragma warning disable 618

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Collections.Extensions
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class AddingTests
    {
        [Fact]
        public void GetOrAdd_ElementNotExisting_StoresAndReturnsElement()
        {
            // arrange
            var element = new TestClass();
            var dict = new Dictionary<string, TestClass>();

            // act
            var result = dict.GetOrAdd("foo", () => element);

            // assert
            result.Should().Be(element); // Referenz-Vergleich zum Testen, dass das wirklich das Element ist
        }

        [Fact]
        public void GetOrAdd_ElementExisting_ReturnsExistingElement()
        {
            // arrange
            var existing = new TestClass();
            var dict = new Dictionary<string, TestClass>();
            dict.Add("foo", existing);

            // act
            var result = dict.GetOrAdd("foo", () => new TestClass());

            // assert
            result.Should().Be(existing); // Referenz-Vergleich zum Testen, dass das wirklich das Element ist
        }

        [Fact]
        public void AddIfMissing_Empty_ShouldAdd()
        {
            // arrange
            var dict = new Dictionary<string, int>();

            // act
            dict.AddIfMissing("test", t => t.Length);

            // assert
            dict.Should().HaveCount(1);
            dict.Should().ContainKey("test");
            dict["test"].Should().Be(4);
        }

        [Fact]
        public void AddIfMissing_NewKey_ShouldAdd()
        {
            // arrange
            var dict = new Dictionary<string, int>
            {
                { "fubar", 5 }
            };

            // act
            dict.AddIfMissing("test", t => t.Length);

            // assert
            dict.Should().HaveCount(2);
            dict.Should().ContainKey("test");
            dict["test"].Should().Be(4);
        }

        [Fact]
        public void AddIfMissing_ExistingKey_ShouldIgnore()
        {
            // arrange
            var dict = new Dictionary<string, int>
            {
                { "fubar", 5 }
            };

            // act
            dict.AddIfMissing("fubar", t => 16);

            // assert
            dict.Should().HaveCount(1);
            dict.Should().ContainKey("fubar");
            dict["fubar"].Should().Be(5);
        }

        [Fact]
        public void TryAdd_ValueProviderAndEmptyDictionary_ShouldAdd()
        {
            // arrange
            var dict = new Dictionary<string, int>();

            // act
            var result = dict.TryAdd("test", () => 4);

            // assert
            result.Should().BeTrue();
            dict.Should().HaveCount(1);
            dict.Should().ContainKey("test");
            dict["test"].Should().Be(4);
        }

        [Fact]
        public void TryAdd_ValueProviderAndNewKey_ShouldAdd()
        {
            // arrange
            var dict = new Dictionary<string, int>
            {
                { "fubar", 5 }
            };

            // act
            var result = dict.TryAdd("test", () => 4);

            // asser
            result.Should().BeTrue();
            dict.Should().HaveCount(2);
            dict.Should().ContainKey("test");
            dict["test"].Should().Be(4);
        }

        [Fact]
        public void TryAdd_ValueProviderAndExistingKey_ShouldIgnore()
        {
            // arrange
            var dict = new Dictionary<string, int>
            {
                { "fubar", 5 }
            };

            // act
            var result = dict.TryAdd("fubar", () => 16);

            // assert
            result.Should().BeFalse();
            dict.Should().HaveCount(1);
            dict.Should().ContainKey("fubar");
            dict["fubar"].Should().Be(5);
        }

        [Fact]
        public void TryAdd_KeyValueProviderAndEmptyDictionary_ShouldAdd()
        {
            // arrange
            var dict = new Dictionary<string, int>();

            // act
            var result = dict.TryAdd("test", t => t.Length);

            // assert
            result.Should().BeTrue();
            dict.Should().HaveCount(1);
            dict.Should().ContainKey("test");
            dict["test"].Should().Be(4);
        }

        [Fact]
        public void TryAdd_KeyValueProviderAndNewKey_ShouldAdd()
        {
            // arrange
            var dict = new Dictionary<string, int>
            {
                { "fubar", 5 }
            };

            // act
            var result = dict.TryAdd("test", t => t.Length);

            // asser
            result.Should().BeTrue();
            dict.Should().HaveCount(2);
            dict.Should().ContainKey("test");
            dict["test"].Should().Be(4);
        }

        [Fact]
        public void TryAdd_KeyValueProviderAndExistingKey_ShouldIgnore()
        {
            // arrange
            var dict = new Dictionary<string, int>
            {
                { "fubar", 5 }
            };

            // act
            var result = dict.TryAdd("fubar", t => 16);

            // assert
            result.Should().BeFalse();
            dict.Should().HaveCount(1);
            dict.Should().ContainKey("fubar");
            dict["fubar"].Should().Be(5);
        }

        [Fact]
        public void Add_NoValue_ShouldBeStable()
        {
            // arrange
            var enumerable = Enumerable.Range(1, 3);

            // act
            var result = enumerable.Add().ToList();

            // assert
            result.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void Add_SingleValue_ShouldAdd()
        {
            // arrange
            var enumerable = Enumerable.Range(1, 3);

            // act
            var result = enumerable.Add(4).ToList();

            // assert
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
        }

        [Fact]
        public void Add_MultipleValues_ShouldAdd()
        {
            // arrange
            var enumerable = Enumerable.Range(1, 3);

            // act
            var result = enumerable.Add(4, 5, 6).ToList();

            // assert
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Fact]
        public void Add_FixedSizeListGiven_ReturnsOtherInstance()
        {
            // arrange
            var list = new[] { 1, 2, 3 };

            // act
            var result = list.Add(4, 5, 6);

            // assert
            result.Should().NotBeSameAs(list);
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Fact]
        public void Add_NotFixedSizeListGiven_ReturnsSameInstance()
        {
            // arrange
            var list = new List<int> { 1, 2, 3 };

            // act
            var result = list.Add(4, 5, 6);

            // assert
            result.Should().BeSameAs(list);
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Fact]
        public void Append_NoValue_ShouldBeStable()
        {
            // arrange
            var enumerable = Enumerable.Range(1, 3);

            // act
            var result = enumerable.Append().ToList();

            // assert
            result.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void Append_SingleValue_ShouldAppend()
        {
            // arrange
            var enumerable = Enumerable.Range(1, 3);

            // act
            var result = enumerable.Append(4).ToList();

            // assert
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
        }

        [Fact]
        public void Append_MultipleValues_ShouldAppend()
        {
            // arrange
            var enumerable = Enumerable.Range(1, 3);

            // act
            var result = enumerable.Append(4, 5, 6).ToList();

            // assert
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Fact]
        public void AddOrReplace_NotMatch_AddsEntry()
        {
            // arrange
            var collection = new[] { 1, 2, 3 }.ToList();

            // act
            collection.AddOrReplace(e => e == 4, 4);

            // assert
            collection.Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
        }

        [Fact]
        public void AddOrReplace_Match_ReplacesEntry()
        {
            // arrange
            var collection = new[] { 1, 2, 3 }.ToList();

            // act
            collection.AddOrReplace(e => e == 2, 4);

            // assert
            collection.Should().BeEquivalentTo(new[] { 1, 4, 3 });
        }

        [Fact]
        public void AddOrReplace_MatchAndTryToExchangeSame_ChangesNothing()
        {
            // arrange
            var collection = new[] { 1, 2, 3 }.ToList();

            // act
            collection.AddOrReplace(e => e == 2, 2);

            // assert
            collection.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void AddOrReplace_MultipleMatches_ThrowsException()
        {
            // arrange
            var collection = new[] { 1, 2, 2 }.ToList();

            Action fail = () => collection.AddOrReplace(e => e == 2, 4);

            // act + assert
            fail.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void AddOrReplaceAll_ICollectionWithNoMatches_AppendsElement()
        {
            // arrange
            var collection = new Mock<ICollection<int>>();
            collection.Setup(c => c.GetEnumerator()).Returns(new List<int> { 1, 2, 3 }.GetEnumerator());

            // act
            collection.Object.AddOrReplaceAll(4, 5);

            // assert
            collection.Verify(c => c.Add(5), Times.Once);
            collection.Verify(c => c.Add(It.Is<int>(i => i != 5)), Times.Never);
            collection.Verify(c => c.Remove(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void AddOrReplaceAll_ICollectionWithOneMatch_AppendsElement()
        {
            // arrange
            var collection = new Mock<ICollection<int>>();
            collection.Setup(c => c.GetEnumerator()).Returns(new List<int> { 1, 2, 3 }.GetEnumerator());

            // act
            collection.Object.AddOrReplaceAll(2, 5);

            // assert
            collection.Verify(c => c.Add(5), Times.Once);
            collection.Verify(c => c.Add(It.Is<int>(i => i != 5)), Times.Never);
            collection.Verify(c => c.Remove(2), Times.Once);
            collection.Verify(c => c.Remove(It.Is<int>(i => i != 2)), Times.Never);
        }

        [Fact]
        public void AddOrReplaceAll_ICollectionWithOneMatchAndExisting_AppendsElement()
        {
            // arrange
            var collection = new Mock<ICollection<int>>();
            collection.Setup(c => c.GetEnumerator()).Returns(new List<int> { 1, 2, 3 }.GetEnumerator());

            // act
            collection.Object.AddOrReplaceAll(2, 1);

            // assert
            collection.Verify(c => c.Add(1), Times.Once);
            collection.Verify(c => c.Add(It.Is<int>(i => i != 1)), Times.Never);
            collection.Verify(c => c.Remove(2), Times.Once);
            collection.Verify(c => c.Remove(It.Is<int>(i => i != 2)), Times.Never);
        }

        [Fact]
        public void AddOrReplaceAll_ICollectionWithOneMatchAndEquals_DoesNothing()
        {
            // arrange
            var collection = new Mock<ICollection<int>>();
            collection.Setup(c => c.GetEnumerator()).Returns(new List<int> { 1, 2, 3 }.GetEnumerator());

            // act
            collection.Object.AddOrReplaceAll(2, 2);

            // assert
            collection.Verify(c => c.Add(It.IsAny<int>()), Times.Never);
            collection.Verify(c => c.Remove(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void AddOrReplaceAll_ICollectionWithMultipleMatch_AppendsElement()
        {
            // arrange
            var collection = new Mock<ICollection<int>>();
            collection.Setup(c => c.GetEnumerator()).Returns(new List<int> { 1, 2, 3 }.GetEnumerator());

            // act
            collection.Object.AddOrReplaceAll(c => true, 5);

            // assert
            collection.Verify(c => c.Add(5), Times.Exactly(3));
            collection.Verify(c => c.Add(It.Is<int>(i => i != 5)), Times.Never);
            collection.Verify(c => c.Remove(1), Times.Once);
            collection.Verify(c => c.Remove(2), Times.Once);
            collection.Verify(c => c.Remove(3), Times.Once);
            collection.Verify(c => c.Remove(It.Is<int>(i => i < 1 || i > 3)), Times.Never);
        }

        [Fact]
        public void AddOrReplaceAll_ICollectionWithMultipleMatchAndExistingReplacement_PreservesExisting()
        {
            // arrange
            var collection = new Mock<ICollection<int>>();
            collection.Setup(c => c.GetEnumerator()).Returns(new List<int> { 1, 2, 3 }.GetEnumerator());

            // act
            collection.Object.AddOrReplaceAll(c => true, 2);

            // assert
            collection.Verify(c => c.Add(2), Times.Exactly(2));
            collection.Verify(c => c.Add(It.Is<int>(i => i != 2)), Times.Never);
            collection.Verify(c => c.Remove(1), Times.Once);
            collection.Verify(c => c.Remove(3), Times.Once);
            collection.Verify(c => c.Remove(It.Is<int>(i => i < 1 || i > 3)), Times.Never);
        }

        [Fact]
        public void AddOrReplaceAll_ArrayWithNoMatches_ThrowsException()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            Action fail = () => arr.AddOrReplaceAll(4, 5);

            // act + assert
            fail.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void AddOrReplaceAll_ArrayWithOnMatch_ReplacesElementOnIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            arr.AddOrReplaceAll(2, 5);

            // assert
            arr.Should().HaveCount(3);
            arr[1].Should().Be(5);
        }

        [Fact]
        public void AddOrReplaceAll_ArrayWithMultipleMatches_ReplacesElementsOnIndex()
        {
            // arrange
            var arr = new[] { 1, 2, 3 };

            // act
            arr.AddOrReplaceAll(a => true, 5);

            // assert
            arr.Should().HaveCount(3);
            arr.Should().OnlyContain(i => i == 5);
        }

        [Fact]
        public void AddOrReplaceAll_ListWithNoMatches_AppendsElement()
        {
            // arrange
            var list = new List<int> { 1, 2, 3 };

            // act
            list.AddOrReplaceAll(4, 5);

            // assert
            list.Should().HaveCount(4);
            list[3].Should().Be(5);
        }

        [Fact]
        public void AddOrReplaceAll_ListWithOnMatch_ReplacesElementOnIndex()
        {
            // arrange
            var list = new List<int> { 1, 2, 3 };

            // act
            list.AddOrReplaceAll(2, 5);

            // assert
            list.Should().HaveCount(3);
            list[1].Should().Be(5);
        }

        [Fact]
        public void AddOrReplaceAll_ListWithMultipleMatches_ReplacesElementsOnIndex()
        {
            // arrange
            var list = new List<int> { 1, 2, 3 };

            // act
            list.AddOrReplaceAll(a => true, 5);

            // assert
            list.Should().HaveCount(3);
            list.Should().OnlyContain(i => i == 5);
        }

        private class TestClass
        {
        }
    }
}