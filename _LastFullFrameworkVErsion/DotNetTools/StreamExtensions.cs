using System;
using System.IO;

namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Extension-Methoden zum leichteren Umgang mit Streams.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Liest den Stream von der aktuellen Position bis zum Ende.
        /// </summary>
        /// <param name="stream">Stream, der gelesen werden soll.</param>
        /// <returns>Array von Bytes, die aus dem Stream gelesen wurden.</returns>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            // Implementierung von:
            // http://stackoverflow.com/questions/221925/creating-a-byte-array-from-a-stream

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}