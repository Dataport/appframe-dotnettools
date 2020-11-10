using System.IO;
using System.Text;

namespace Dataport.AppFrameDotNet.DotNetTools.IO.Extensions
{
    /// <summary>
    /// Stellt Methoden zur Konvertierung von String in IO-Elemente konvertieren.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Überführt einen String in einen Stream.
        /// </summary>
        /// <param name="str">Der zu verarbeitende String.</param>
        /// <param name="encoding">Das Encoding des Strings.</param>
        /// <returns>Stream-Representation des Strings.</returns>
        public static Stream AsStream(this string str, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(str));
        }
    }
}