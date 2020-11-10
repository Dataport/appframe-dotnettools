using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dataport.AppFrameDotNet.DotNetTools.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Linq
{
    [TestClass]
    public class PropertySelectorToolsTests
    {
        [TestMethod]
        public void PropertySelectorTools_SelectPropertyByPath_Simple()
        {
            var daten = new List<Model>()
            {
                new Model() {Id = 10, Content = "Hallo"},
                new Model() {Id = 5, Content = "Welt"},
                new Model() {Id = 3, Content = "Blup"},
                new Model() {Id = 7, Content = "Bla"},
                new Model() {Id = 13, Content = "Ende"},
            }.AsQueryable();

            var result = daten.OrderBy(PropertySelectorTools.SelectPropertyByPath<Model, int>("Id"));
        }

        /// <summary>
        /// Wenn explizit eine ParameterExpression angegeben wird, sollte
        /// diese in der erzeugen Expression verwendet werden.
        /// </summary>
        [TestMethod]
        public void PropertySelectorTools_SelectPropertyByPath_ExplicitParameter()
        {
            var parameter = Expression.Parameter(typeof(Model), "x");
            var result = PropertySelectorTools.SelectPropertyByPath<Model, string>("Content", parameter);

            Assert.IsNotNull(result, "result");
            Assert.AreEqual(1, result.Parameters.Count, "result.Parameters.Count");
            Assert.AreSame(parameter, result.Parameters.First());
        }

        /// <summary>
        /// Wenn als explizit angegebene ParameterExpression null übergeben wird,
        /// sollte eine ArgumentNullException geworfen werden.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PropertySelectorTools_SelectPropertyByPath_ExplicitParameter_Null()
        {
            PropertySelectorTools.SelectPropertyByPath<Model, string>("Content", null);
        }

        /// <summary>
        /// Wenn eine explizit angegebene ParameterExpression einen falschen Typen aufweist,
        /// sollte eine InvalidOperationException geworfen werden.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PropertySelectorTools_SelectPropertyByPath_ExplicitParameter_TypeMismatch()
        {
            var parameter = Expression.Parameter(typeof(object), "x");
            PropertySelectorTools.SelectPropertyByPath<Model, string>("Content", parameter);
        }

        private class Model
        {
            public int Id { get; set; }
            public string Content { get; set; }
        }
    }
}
