using System.IO;

namespace Dataport.AppFrameDotNet.DotNetTools.IO.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Bearbeiten von Streams zur Verfügung.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Überführt den Stream in ein Byte-Array.
        /// </summary>
        /// <param name="stream">Der Stream der überführt werden soll.</param>
        /// <returns>Byte-Representation des Streams</returns>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return memoryStream.ToArray();
            }

            // Stream.CopyTo() startet an der aktuellen Position
            // MemoryStream.ToArray() ignoriert die aktuelle Position
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            using (memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}