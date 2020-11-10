using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests
{
    /// <summary>
    /// Tests zu den in EnumExtensions definierten Extension-Methoden.
    /// </summary>
    [TestClass]
    public class EnumExtensionsTests
    {
        /// <summary>
        /// Wenn kein Name über ein Display-Attribut angegeben wurde, 
        /// soll ToDisplayName den Bezeichner des Enum-Wertes zurückgeben.
        /// </summary>
        [TestMethod]
        public void EnumExtensions_ToDisplayName_Success()
        {
            var value = TestEnum.Wert1;
            var name = value.ToDisplayName();

            Assert.AreEqual("Wert1", name, "Unerwarteter konvertierter Wert.");
        }

        /// <summary>
        /// Wenn ein Name über ein Display-Attribut angegeben wurde,
        /// soll ToDisplayName diesen zurückgeben.
        /// </summary>
        [TestMethod]
        public void EnumExtensions_ToDisplayName_Success_DisplayName()
        {
            var value = TestEnum.Wert2;
            var name = value.ToDisplayName();

            Assert.AreEqual("DisplayName", name, "Unerwarteter konvertierter Wert.");
        }

        /// <summary>
        /// ToDisplayName sollte damit umgehen können, wenn eine 0
        /// für einen Enum angegeben wird, bei dem manuelle Werte definiert wurden und 0 nicht definiert ist.
        /// In diesem Fall sollte string.Empty zurückgegeben werden.
        /// </summary>
        [TestMethod]
        public void EnumExtensions_ToDisplayName_Success_NullWert()
        {
            TestEnum value = 0;
            var name = value.ToDisplayName();

            Assert.AreEqual(string.Empty, name, "Unerwarteter konvertierter Wert.");
        }

        /// <summary>
        /// Enum zum Testen der Extension-Methoden.
        /// </summary>
        private enum TestEnum
        {
            Wert1 = 1,

            [Display(Name = "DisplayName")]
            Wert2 = 2
        }
    }
}