using System;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Stellt Erweiterungen zum Transformieren von Objekten zur Verfügung
    /// </summary>
    public static class TransformationExtensions
    {
        /// <summary>
        /// Wrappt das übergebene Objekt in eine <see cref="Nullable{T}"/>-Struktur.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Objects</typeparam>
        /// <param name="value">Das Objekt das gewrappt werden soll.</param>
        /// <returns>Das gewrappte Objekt</returns>
        public static TType? AsNullable<TType>(this TType value) where TType : struct
        {
            return value;
        }

        /// <summary>
        /// Wrappt das übergebene Objekt in ein Array des Datentyps.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Objects</typeparam>
        /// <param name="value">Das Objekt das gewrappt werden soll.</param>
        /// <param name="nullAsEmpty">Wenn <see langword="true"/> wird ein leeres Array erzeugt, wenn <paramref name="value"/> <see langword="null"/> ist.</param>
        /// <returns>Array des Datentyps.</returns>
        public static TType[] AsArray<TType>(this TType value, bool nullAsEmpty = false)
        {
            return nullAsEmpty && value == null ? Array.Empty<TType>() : new[] { value };
        }

        /// <summary>
        /// Wrappt das übergebene Objekt in ein Array des Zieltyps.
        /// </summary>
        /// <typeparam name="TIn">Der Typ des Objects</typeparam>
        /// <typeparam name="TOut">Der Zieltyp</typeparam>
        /// <param name="value">Das Objekt das gewrappt werden soll.</param>
        /// <param name="nullAsEmpty">Wenn <see langword="true"/> wird ein leeres Array erzeugt, wenn <paramref name="value"/> <see langword="null"/> ist.</param>
        /// <returns>Array des Zieltyps.</returns>
        public static TOut[] AsArrayOf<TIn, TOut>(this TIn value, bool nullAsEmpty = false) where TIn : TOut
        {
            return nullAsEmpty && value == null ? Array.Empty<TOut>() : new TOut[] { value };
        }
    }
}