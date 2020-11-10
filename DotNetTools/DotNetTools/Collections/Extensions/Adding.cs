using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Validation;
using Dataport.AppFrameDotNet.DotNetTools.Validation.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Hinzufügen von Elementen zur Verfügung.
    /// </summary>
    public static class Adding
    {
        /// <summary>
        /// Fügt einer Enumeration verschiedene Werte hinzu.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="additionalValues">Die Werte, die hinzugeführt werden sollen.</param>
        /// <returns>Enumeration der verketteten Werte</returns>
        public static IEnumerable<TType> Add<TType>(this IEnumerable<TType> enumerable, params TType[] additionalValues)
        {
            if (enumerable is IList list && !list.IsFixedSize)
            {
                additionalValues.ForEach(v => list.Add(v));
                return enumerable;
            }
            return enumerable.Concat(additionalValues);
        }

        /// <summary>
        /// Fügt einer Enumeration verschiedene Werte hinzu.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="additionalValues">Die Werte die hinzugeführt werden sollen</param>
        /// <returns>Enumeration der verketteten Werte</returns>
        public static IEnumerable<TType> Append<TType>(this IEnumerable<TType> enumerable, params TType[] additionalValues)
        {
            return enumerable.Concat(additionalValues);
        }

        /// <summary>
        /// Gibt den Eintrag im Wörterbuch zurück und legt diesen an, wenn er noch nicht vorhanden ist.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="dictionary">Das verwendete Wörterbuch</param>
        /// <param name="key">Der Schlüssel anhand dessen der Wert ermittelt oder hinzugefügt werden soll</param>
        /// <param name="valueProvider">Gibt den Wert zurück</param>
        /// <returns>Der Wert im Wörterbuch</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueProvider)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, valueProvider());
            }

            return dictionary[key];
        }

        /// <summary>
        /// Fügt den Eintrag in das <paramref name="dictionary"/> hinzu, wenn der Schlüssel nicht existiert.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="dictionary">Das verwendete Wörterbuch</param>
        /// <param name="key">Der Schlüssel anhand dessen der Wert ermittelt oder hinzugefügt werden soll</param>
        /// <param name="valueProvider">Gibt den Wert zurück</param>
        [Obsolete("Wird mit Version 4 obsolet, verwende stattdessen TryAdd")]
        public static void AddIfMissing<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueProvider)
        {
            dictionary.TryAdd(key, valueProvider);
        }

        /// <summary>
        /// Versucht einen Eintrag in das <paramref name="dictionary"/> hinzuzufügen, wenn der Schlüssel nicht existiert.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="dictionary">Das verwendete Wörterbuch</param>
        /// <param name="key">Der Schlüssel anhand dessen der Wert ermittelt oder hinzugefügt werden soll</param>
        /// <param name="valueProvider">Gibt den Wert zurück</param>
        /// <returns><see langword="true"/> wenn das Hinzufügen erfolgreich war, andernfalls <see langword="false"/></returns>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueProvider)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, valueProvider());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Versucht einen Eintrag in das <paramref name="dictionary"/> hinzuzufügen, wenn der Schlüssel nicht existiert.
        /// </summary>
        /// <typeparam name="TKey">Der Schlüsseltyp des Wörterbuchs</typeparam>
        /// <typeparam name="TValue">Der Wertetyp des Wörterbuchs</typeparam>
        /// <param name="dictionary">Das verwendete Wörterbuch</param>
        /// <param name="key">Der Schlüssel anhand dessen der Wert ermittelt oder hinzugefügt werden soll</param>
        /// <param name="valueProvider">Gibt den Wert zurück</param>
        /// <returns><see langword="true"/> wenn das Hinzufügen erfolgreich war, andernfalls <see langword="false"/></returns>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueProvider)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, valueProvider(key));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ersetzt einen Eintrag in einer Liste durch einen anderen wenn dieser bereits vorhanden ist.
        /// Ansonsten wir er einfach zugefügt. Für eine Ersetzung aller Vorkommen siehe Methode <see cref="AddOrReplaceAll{TType}(ICollection{TType},Func{TType,bool},TType)" />.
        /// </summary>
        /// <typeparam name="TType">Typ des Listenobjekts</typeparam>
        /// <param name="context">Liste</param>
        /// <param name="selector">Regel zum selektieren des Eintrags. Muss ein einwertiges Ergebnis liefern.</param>
        /// <param name="replacement">Ersatzobjekt</param>
        /// <exception cref="ArgumentNullException">Einer der übergebenen Parameter ist null.</exception>
        /// <exception cref="NotSupportedException">Es wurde eine Collection fester Länge (bspw. ein Array) übergeben.</exception>
        public static void AddOrReplace<TType>(this ICollection<TType> context, Func<TType, bool> selector, TType replacement)
        {
            Verify.That(context, nameof(context)).IsNotNull();
            Verify.That(selector, nameof(selector)).IsNotNull();
            Verify.That(replacement, nameof(replacement)).IsNotNull();

            var existing = context.SingleOrDefault(selector);

            if (existing != null)
            {
                if (typeof(TType).Implements(typeof(IComparable<TType>)) && ((IComparable<TType>)existing).CompareTo(replacement) == 0)
                {
                    return;
                }

                context.Remove(existing);
            }

            context.Add(replacement);
        }

        /// <summary>
        /// Ersetzt alle Einträge in einer <see cref="ICollection{T}"/> durch einen anderen wenn dieser bereits vorhanden ist.
        /// Ansonsten wir er einfach ans Ende hinzugefügt.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Collection</typeparam>
        /// <param name="collection">Die Collection die modifiziert wird.</param>
        /// <param name="oldValue">Der alte, zu ersetzende, Wert.</param>
        /// <param name="replacement">Der neue Wert der eingetragen werden soll.</param>
        /// <exception cref="NotSupportedException">Wenn eine Collection fester Länge verwendet wird.</exception>
        public static void AddOrReplaceAll<TType>(this ICollection<TType> collection, TType oldValue, TType replacement) where TType : IComparable
        {
            collection.AddOrReplaceAll(v => v.CompareTo(oldValue) == 0, replacement);
        }

        /// <summary>
        /// Ersetzt alle Einträge in einer <see cref="ICollection{T}"/> durch einen anderen wenn dieser bereits vorhanden ist.
        /// Ansonsten wird er einfach ans Ende hinzugefügt.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Collection</typeparam>
        /// <param name="collection">Die Collection die modifiziert wird.</param>
        /// <param name="selector">Prädikat anhand dessen Werte ausgewählt werden können.</param>
        /// <param name="replacement">Der neue Wert der eingetragen werden soll.</param>
        /// <exception cref="NotSupportedException">Wenn eine Collection fester Länge verwendet wird.</exception>
        public static void AddOrReplaceAll<TType>(this ICollection<TType> collection, Func<TType, bool> selector, TType replacement)
        {
            var elements = collection.Where(selector).ToArray();

            if (elements.Length > 0)
            {
                bool isComparable = typeof(TType).Implements(typeof(IComparable<TType>));
                foreach (var element in elements)
                {
                    if (isComparable && ((IComparable<TType>)element).CompareTo(replacement) == 0)
                    {
                        continue;
                    }

                    collection.Remove(element); // muss selbst bei doppelten Einträgen mehrfach aufgerufen werden
                    collection.Add(replacement); // fügt am Ende an, anders geht es für ICollection leider nicht
                }
            }
            else
            {
                collection.Add(replacement);
            }
        }

        /// <summary>
        /// Ersetzt alle Einträge in einer <see cref="IList{T}"/> positionsgerecht durch einen anderen wenn dieser bereits vorhanden ist.
        /// Ansonsten wir er einfach ans Ende hinzugefügt.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Liste</typeparam>
        /// <param name="list">Die Liste die modifiziert wird.</param>
        /// <param name="oldValue">Der alte, zu ersetzende, Wert.</param>
        /// <param name="replacement">Der neue Wert der eingetragen werden soll.</param>
        public static void AddOrReplaceAll<TType>(this IList<TType> list, TType oldValue, TType replacement) where TType : IComparable
        {
            list.AddOrReplaceAll(v => v.Equals(oldValue), replacement);
        }

        /// <summary>
        /// Ersetzt alle Einträge in einer <see cref="IList{T}"/> positionsgerecht durch einen anderen wenn dieser bereits vorhanden ist.
        /// Ansonsten wir er einfach ans Ende hinzugefügt.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Liste</typeparam>
        /// <param name="list">Die Liste die modifiziert wird.</param>
        /// <param name="selector">Prädikat anhand dessen Werte ausgewählt werden können.</param>
        /// <param name="replacement">Der neue Wert der eingetragen werden soll.</param>
        public static void AddOrReplaceAll<TType>(this IList<TType> list, Func<TType, bool> selector, TType replacement)
        {
            var indexes = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (selector(list[i]))
                {
                    indexes.Add(i);
                }
            }

            if (indexes.Count > 0)
            {
                foreach (var index in indexes)
                {
                    list[index] = replacement;
                }
            }
            else
            {
                list.Add(replacement);
            }
        }
    }
}