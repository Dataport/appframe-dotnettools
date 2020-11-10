using System;
using System.Collections.Generic;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Linq
{
    [TestClass]
    public class PagingExtensionsTests
    {
        [TestMethod]
        public void PagingExtensionsTests_Page()
        {
            var daten = new List<Model>()
            {
                new Model() {Id = 10, Content = "Hallo"},
                new Model() {Id = 5, Content = "Welt"},
                new Model() {Id = 3, Content = "Blup"},
                new Model() {Id = 7, Content = "Bla"},
                new Model() {Id = 13, Content = "Ende"},
            }.OrderBy(x => x.Id);

            var page0 = daten.Page(2, 0);

            Assert.AreEqual(2, page0.Items.Count(), "page0: Items.Count");
            Assert.AreEqual(3, page0.Items.First().Id, "page0: Items.First().Id");
            Assert.AreEqual(5, page0.Items.Last().Id, "page0: Items.Last().Id");
            Assert.AreEqual(3, page0.PageCount, "page0: PageCount");
            Assert.AreEqual(5, page0.TotalItemCount, "page0: TotalItemCount");
            Assert.AreEqual(0, page0.CurrentPage, "page0: CurrentPage");

            var page1 = daten.Page(2, 1);

            Assert.AreEqual(2, page1.Items.Count(), "page1: Items.Count");
            Assert.AreEqual(7, page1.Items.First().Id, "page1: Items.First().Id");
            Assert.AreEqual(10, page1.Items.Last().Id, "page1: Items.Last().Id");
            Assert.AreEqual(3, page1.PageCount, "page1: PageCount");
            Assert.AreEqual(5, page1.TotalItemCount, "page1: TotalItemCount");
            Assert.AreEqual(1, page1.CurrentPage, "page1: CurrentPage");

            var page2 = daten.Page(2, 2);

            Assert.AreEqual(1, page2.Items.Count(), "page2: Items.Count");
            Assert.AreEqual(13, page2.Items.First().Id, "page2: Items.First().Id");
            Assert.AreEqual(3, page2.PageCount, "page2: PageCount");
            Assert.AreEqual(5, page2.TotalItemCount, "page2: TotalItemCount");
            Assert.AreEqual(2, page2.CurrentPage, "page2: CurrentPage");
        }

        [TestMethod]
        public void PagingExtensionsTests_Page_ZeroItems()
        {
            var daten = new List<Model>() { }.AsQueryable().OrderBy(x => x.Id);

            var page0 = daten.Page(2, 0);

            Assert.AreEqual(0, page0.Items.Count(), "page0: Items.Count");
            Assert.AreEqual(0, page0.PageCount, "page0: PageCount");
            Assert.AreEqual(0, page0.TotalItemCount, "page0: TotalItemCount");
            Assert.AreEqual(-1, page0.CurrentPage, "page0: CurrentPage");
        }

        [TestMethod]
        public void PagingExtensionsTests_Page_RequestedPageOutOfRange()
        {
            var daten = new List<Model>()
            {
                new Model() {Id = 10, Content = "Hallo"},
                new Model() {Id = 5, Content = "Welt"},
                new Model() {Id = 3, Content = "Blup"},
                new Model() {Id = 7, Content = "Bla"},
                new Model() {Id = 13, Content = "Ende"},
            }.OrderBy(x => x.Id); ;

            var page0 = daten.Page(2, 7);

            Assert.AreEqual(0, page0.Items.Count(), "page0: Items.Count");
            Assert.AreEqual(3, page0.PageCount, "page0: PageCount");
            Assert.AreEqual(5, page0.TotalItemCount, "page0: TotalItemCount");
            Assert.AreEqual(-1, page0.CurrentPage, "page0: CurrentPage");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PagingExtensionsTests_Page_NegativeRequestedPage()
        {
            var daten = new List<Model>() { }.AsQueryable().OrderBy(x => x.Id); ;

            daten.Page(2, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PagingExtensionsTests_Page_UnplausiblePageSite()
        {
            var daten = new List<Model>() { }.AsQueryable().OrderBy(x => x.Id); ;

            daten.Page(0, 1);
        }

        private class Model
        {
            public int Id { get; set; }
            public string Content { get; set;  }
        }
    }
}
