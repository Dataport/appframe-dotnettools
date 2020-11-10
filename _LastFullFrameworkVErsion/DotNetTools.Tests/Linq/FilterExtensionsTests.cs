using System.Collections.Generic;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Linq
{
    [TestClass]
    public class FilterExtensionsTests
    {
        private readonly IQueryable<IntDataObject> _intData = new List<IntDataObject> {
            new IntDataObject() { Data =  1},
            new IntDataObject() { Data =  2},
            new IntDataObject() { Data =  3}
        }.AsQueryable();

        [TestMethod]
        public void FilterExtensions_ApplyOptionalEqualityFilter_Apply  ()
        {
            var result = _intData.ApplyOptionalEqualityFilter(x => x.Data, 2);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void FilterExtensions_ApplyOptionalEqualityFilter_DontApply()
        {
            var result = _intData.ApplyOptionalEqualityFilter(x => x.Data, null);
            Assert.AreEqual(3, result.Count());
        }

        private class IntDataObject
        {
            public int Data { get; set; }
        }

        private readonly IQueryable<StringDataObject> _stringData = new List<StringDataObject> {
            new StringDataObject() { Data =  "1"},
            new StringDataObject() { Data =  "2"},
            new StringDataObject() { Data =  "3"}
        }.AsQueryable();

        [TestMethod]
        public void FilterExtensions_ApplyOptionalWhereLikeFilter_Apply()
        {
            var result = _stringData.ApplyOptionalWhereLikeFilter(x => x.Data, "2");
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void FilterExtensions_ApplyOptionalWhereLikeFilter_DontApply()
        {
            var result = _stringData.ApplyOptionalWhereLikeFilter(x => x.Data, "    ");
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void FilterExtensions_ApplyOptionalWhereFilter_Apply()
        {
            int? filterValue = 2;
            var result = _intData.ApplyOptionalWhereFilter(x => x.Data > filterValue, filterValue);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result.Single().Data);
        }

        [TestMethod]
        public void FilterExtensions_ApplyOptionalWhereFilter_DontApply()
        {
            int? filterValue = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var result = _intData.ApplyOptionalWhereFilter(x => x.Data > filterValue, filterValue);
            Assert.AreEqual(3, result.Count());
        }

        private class StringDataObject
        {
            public string Data { get; set; }
        }
    }
}
