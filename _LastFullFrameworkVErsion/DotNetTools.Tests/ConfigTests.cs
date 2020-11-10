using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void ConfigTests_GetAppSetting_String()
        {
            var result = Config.GetAppSetting("String");
            Assert.AreEqual("Hallo", result);
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_StringEmpty()
        {
            var result = Config.GetAppSetting("StringEmpty");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_StringOhneWert()
        {
            var result = Config.GetAppSetting("StringOhneWert");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_Bool()
        {
            var result = Config.GetAppSetting<bool>("Bool");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_WithDefault_Bool()
        {
            var result = Config.GetAppSetting<bool>("Bool", false);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigTests_GetAppSetting_BoolEmpty()
        {
            var result = Config.GetAppSetting<bool>("BoolEmpty");
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_BoolEmpty_WithDefault()
        {
            var result = Config.GetAppSetting<bool>("BoolEmpty", false);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigTests_GetAppSetting_BoolOhneWert()
        {
            var result = Config.GetAppSetting<bool>("BoolOhneWert");
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_BoolOhneWert_WithDefault()
        {
            var result = Config.GetAppSetting<bool>("BoolOhneWert", false);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigTests_GetAppSetting_BoolNichtKonvertierbar()
        {
            var result = Config.GetAppSetting<bool>("BoolNichtKonvertierbar");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigTests_GetAppSetting_BoolNichtKonvertierbar_WithDefault()
        {
            var result = Config.GetAppSetting<bool>("BoolNichtKonvertierbar", false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigTests_GetAppSetting_Fehlt()
        {
            var result = Config.GetAppSetting("Fehlt");
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_Fehlt_WithDefault()
        {
            var result = Config.GetAppSetting("Fehlt", "Hallo");
            Assert.AreEqual("Hallo", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigTests_GetAppSetting_FehltMitCast()
        {
            var result = Config.GetAppSetting<bool>("Fehlt");
        }

        [TestMethod]
        public void ConfigTests_GetAppSetting_FehltMitCast_WithDefault()
        {
            var result = Config.GetAppSetting<bool>("Fehlt", true);
            Assert.AreEqual(true, result);
        }
    }
}
