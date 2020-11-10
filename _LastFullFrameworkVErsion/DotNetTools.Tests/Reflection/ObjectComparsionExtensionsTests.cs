using Dataport.AppFrameDotNet.DotNetTools.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection
{
    [TestClass]
    public class ObjectComparsionExtensionsTests
    {
        [TestMethod]
        public void ObjectComparsionExtensions_IsEqualOnPropertyLevel_Equal()
        {
            var source = new Testklasse() { Text="Hallo", Nummer = 1, Egal = "Eins"};
            var target = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Zwei" };

            Assert.AreEqual(true, source.IsEqualOnPropertyLevel(target, typeof(IgnoreInPropertyComparsionAttribute)));
        }

        [TestMethod]
        public void ObjectComparsionExtensions_IsEqualOnPropertyLevel_NotEqual()
        {
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = "Hallo", Nummer = 2, Egal = "Zwei" };

            Assert.AreEqual(false, source.IsEqualOnPropertyLevel(target, typeof(IgnoreInPropertyComparsionAttribute)));
        }

        [TestMethod]
        public void ObjectComparsionExtensions_IsEqualOnPropertyLevel_NotEqual_WithResult()
        {
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = "Hallo", Nummer = 2, Egal = "Zwei" };

            MemberComparisonResult[] results = null;

            Assert.AreEqual(false, 
                source.IsEqualOnPropertyLevel(target, ref results, typeof(IgnoreInPropertyComparsionAttribute)));
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("Nummer", results[0].MemberName);
            Assert.AreEqual(1, results[0].SourceValue);
            Assert.AreEqual(2, results[0].TargetValue);
        }

        [TestMethod]
        public void ObjectComparsionExtensions_MergeWith()
        {
            var source = new Testklasse() { Text = "Hallo", Nummer = 1, Egal = "Eins" };
            var target = new Testklasse() { Text = null, Nummer = 0, Egal = "Zwei" };

            target.MergeWith(source);

            Assert.AreEqual("Hallo", target.Text);
            Assert.AreEqual(1, target.Nummer);
            Assert.AreEqual("Zwei", target.Egal);
        }

        private class Testklasse
        {
            public string Text { get; set; }

            public int Nummer { get; set; }

            [IgnoreInPropertyComparsion]
            public string Egal { get; set; }
        }
    }
}
