using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void StringExtensionsTests_AsCleanDisplayText()
        {
            Assert.AreEqual("Hal\r\nlo", "\"Hal\\r\\nlo\"".AsCleanDisplayText());
        }
    }
}
