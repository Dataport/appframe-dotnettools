using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Collections.Model;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Überführen einer Struktur in eine andere zur Verfügung.
    /// </summary>
    public static class Transformation
    {
        /// <summary>
        /// Transformiert eine Auflistung von <see cref="IGrouping{TKey, TElement}"/> in ein Dictionary.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="collection">Das Representation des Wörterbuchs</param>
        /// <returns>Das erzeugte Dictionary</returns>
        public static IDictionary<TKey, IEnumerable<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> collection)
        {
            return collection.ToDictionary(v => v.Key, v => v.AsEnumerable());
        }

        /// <summary>
        /// Transformiert eine Auflistung von <see cref="IGrouping{TKey, TElement}"/> in ein schreibgeschütztes Dictionary.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="collection">Das Representation des Wörterbuchs</param>
        /// <returns>Das erzeugte Dictionary</returns>
        public static IReadOnlyDictionary<TKey, IEnumerable<TValue>> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> collection)
        {
            return new ReadOnlyDictionary<TKey, IEnumerable<TValue>>(collection.ToDictionary());
        }

        /// <summary>
        /// Transformiert eine Auflistung von <see cref="KeyValuePair{TKey, TValue}"/> in ein Dictionary.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="representation">Das Representation des Wörterbuchs</param>
        /// <returns>Das erzeugte Dictionary</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> representation)
        {
            return representation.ToDictionary(d => d.Key, d => d.Value);
        }

        /// <summary>
        /// Transformiert eine Auflistung von <see cref="KeyValuePair{TKey, TValue}"/> in ein schreibgeschütztes Dictionary.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="representation">Das Representation des Wörterbuchs</param>
        /// <returns>Das erzeugte Dictionary</returns>
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> representation)
        {
            return new ReadOnlyDictionary<TKey, TValue>(representation.ToDictionary());
        }

        /// <summary>
        /// Transformiert die übergebene Enumerable in ein schreibgeschütztes Dictionary.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="source">Die ausgehende Enumeration</param>
        /// <param name="keySelector">Eine Funktion zum Extrahieren eines Schlüssels aus jedem Element.</param>
        /// <returns>Das erzeugte schreibgeschütze Wörterbuch.</returns>
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        {
            return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary(keySelector));
        }

        /// <summary>
        /// Gibt eine Enumeration aller Kombinationen der beiden Enumerationen zurück.
        /// </summary>
        /// <typeparam name="TLeft">Der Typ der linken Enumeration</typeparam>
        /// <typeparam name="TRight">Der Typ der rechten Enumeration</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="collectionToJoin">Die zu joinende Enumeration</param>
        /// <returns>Enumeration aller Kombinationen der beiden Enumerationen</returns>
        public static IEnumerable<Tuple<TLeft, TRight>> CrossJoin<TLeft, TRight>(this IEnumerable<TLeft> enumerable, IEnumerable<TRight> collectionToJoin)
        {
            return enumerable.CrossJoin(collectionToJoin, (l, r) => new Tuple<TLeft, TRight>(l, r));
        }

        /// <summary>
        /// Gibt eine Enumeration aller Kombinationen der beiden Enumerationen zurück.
        /// </summary>
        /// <typeparam name="TLeft">Der Typ der linken Enumeration</typeparam>
        /// <typeparam name="TRight">Der Typ der rechten Enumeration</typeparam>
        /// <typeparam name="TResult">Der Typ finalen Enumeration</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="collectionToJoin">Die zu joinende Enumeration</param>
        /// <param name="transformation">Die Bildungsvorschrift, wie die Kombinationen in <typeparamref name="TResult"/> überführt werden können.</param>
        /// <returns>Enumeration aller Kombinationen der beiden Enumerationen</returns>
        public static IEnumerable<TResult> CrossJoin<TLeft, TRight, TResult>(this IEnumerable<TLeft> enumerable, IEnumerable<TRight> collectionToJoin, Func<TLeft, TRight, TResult> transformation)
        {
            var collection = collectionToJoin.ToArray(); // umgehe multiple Itteration über Enumerable
            foreach (TLeft left in enumerable)
            {
                foreach (TRight right in collection)
                {
                    yield return transformation(left, right);
                }
            }
        }

        /// <summary>
        /// Teilt eine Enumerable in verschiedene Chunks mit der Größe <paramref name="chunkSize"/>
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="chunkSize">Die maximale Größe der Chunks</param>
        /// <returns>Alle erzeugten Chunks</returns>
        public static IEnumerable<IEnumerable<TType>> Chunk<TType>(this IEnumerable<TType> enumerable, int chunkSize)
        {
            if (chunkSize < 1)
            {
                throw new ArgumentException("chunksize must be greater 0");
            }

            int count = 0;
            List<TType> subResult = new List<TType>();
            foreach (var current in enumerable)
            {
                subResult.Add(current);
                if (++count == chunkSize)
                {
                    yield return subResult;
                    count = 0;
                    subResult = new List<TType>();
                }
            }

            if (subResult.Any())
            {
                yield return subResult;
            }
        }

        /// <summary>
        /// Fügt eine Auflistung von Elementen zu einem <see langword="string"/> zusammen.
        /// Verhalten: Die Transformation erfolgt über <see cref="object.ToString"/>.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="separator">Der Delimiter für die einzelnen Elemente der Enumeration.</param>
        /// <returns>Der zusammengeführte String.</returns>
        public static string Join<TType>(this IEnumerable<TType> enumerable, string separator)
        {
            if (enumerable == null)
            {
                return string.Empty;
            }

            return string.Join(separator ?? string.Empty, enumerable.Select(e => e?.ToString()));
        }

        /// <summary>
        /// Fügt eine Auflistung von Elementen zu einem <see langword="string"/> zusammen.
        /// Dabei werden nur Elemente betrachtet, die nicht <see langword="null"/> oder leer sind.
        /// Verhalten: Die Transformation erfolgt über <see cref="object.ToString"/>.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="separator">Der Delimiter für die einzelnen Elemente der Enumeration.</param>
        /// <returns>Der zusammengeführte String.</returns>
        public static string JoinNotEmpty<TType>(this IEnumerable<TType> enumerable, string separator)
        {
            if (enumerable == null)
            {
                return string.Empty;
            }

            return enumerable.Select(e => e?.ToString()).Where(e => !string.IsNullOrWhiteSpace(e)).Join(separator);
        }

        /// <summary>
        /// Hilfsmethode um große Datenmengen in Pages zerlegt durchzugehen.
        /// </summary>
        /// <typeparam name="TType">Typ der Datensätze in IOrderedQueryable</typeparam>
        /// <param name="enumerable">Query</param>
        /// <param name="pageSize">Anzahl Datensätze pro Seite</param>
        /// <param name="requestedPage">Angeforderte Seite (nullbasierter Index)</param>
        /// <returns>Rückgabeobjekt mit den Daten einer Datenseite (Page), der Anzahl der Seiten und der Gesamtzahl der Datensätze.</returns>
        public static PagedResult<TType> Page<TType>(this IOrderedQueryable<TType> enumerable, int pageSize, int requestedPage)
        {
            return enumerable.PageInternal(pageSize, requestedPage);
        }

        /// <summary>
        /// Hilfsmethode um große Datenmengen in Pages zerlegt durchzugehen.
        /// </summary>
        /// <typeparam name="TType">Typ der Datensätze in IOrderedQueryable</typeparam>
        /// <param name="enumerable">Query</param>
        /// <param name="pageSize">Anzahl Datensätze pro Seite</param>
        /// <param name="requestedPage">Angeforderte Seite (nullbasierter Index)</param>
        /// <returns>Rückgabeobjekt mit den Daten einer Datenseite (Page), der Anzahl der Seiten und der Gesamtzahl der Datensätze.</returns>
        public static PagedResult<TType> Page<TType>(this IOrderedEnumerable<TType> enumerable, int pageSize, int requestedPage)
        {
            return enumerable.PageInternal(pageSize, requestedPage);
        }

        /// <summary>
        /// Wrappt die übergebene Enumerable in einer schreibgeschützten Liste.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns>Die schreibgeschützte Liste.</returns>
        public static IReadOnlyList<TType> ToReadOnlyList<TType>(this IEnumerable<TType> enumerable)
        {
            if (enumerable is IReadOnlyList<TType> readOnlyList)
            {
                return readOnlyList;
            }

            return new ReadOnlyCollection<TType>(enumerable.ToList());
        }

        private static PagedResult<TSource> PageInternal<TSource>(this IEnumerable<TSource> context, int pageSize, int requestedPage)
        {
            //PageSize macht mit negativem Wert oder 0 keinen Sinn
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize must be positive.");
            }

            //Ein negativer Index für die Page macht keinen Sinn
            if (requestedPage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "requestedPage must be positive.");
            }

            //Gesamtzahl der Datensätze ermitteln
            var totalItems = context.ToArray();
            var totalItemCount = totalItems.Length;

            //Anzahl Seiten ermitteln
            var pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalItemCount) / pageSize));

            //Datensätze für Seite ermitteln
            var items = totalItems.Skip(requestedPage * pageSize).Take(pageSize).ToArray();

            //Wenn die angeforderte Seite leer ist, sind wir außerhalb des möglichen Rückgabebereichs
            var currentPage = !items.Any() ? -1 : requestedPage;

            //Rückgabe
            return new PagedResult<TSource>(items, pageCount, totalItemCount, pageSize, currentPage);
        }
    }
}