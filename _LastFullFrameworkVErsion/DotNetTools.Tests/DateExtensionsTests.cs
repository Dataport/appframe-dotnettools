using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests
{
    [TestClass]
    public class DateExtensionsTests
    {
        [TestMethod]
        public void DateExtensionsTests_EndOfDay()
        {
            Assert.AreEqual(new DateTime(2000,1,1,23,59,59).AddMilliseconds(999), new DateTime(2000, 1, 1, 12,00,00).EndOfDay());
        }
    }
}
