using System;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Diese Klasse stellt Funktionen zur Verfügung, welche Daten und Indexe anhand ihrer Anordnung in Enumerables zurück gibt.
    /// </summary>
    public static class Indexing
    {
        /// <summary>
        /// Gibt den numerischen Index des ersten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int FirstIndexOf<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetIndexForward(1, condition);
        }

        /// <summary>
        /// Gibt den numerischen Index der ersten Position, die den Substring enthält.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="substring">Der gesuchte Substring.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int FirstIndexOf(this string enumerable, string substring)
        {
            return enumerable.IndexOf(substring, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Gibt den numerischen Index des zweiten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int SecondIndexOf<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetIndexForward(2, condition);
        }

        /// <summary>
        /// Gibt den numerischen Index der zweiten Position, die den Substring enthält.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="substring">Der gesuchte Substring.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int SecondIndexOf(this string enumerable, string substring)
        {
            return enumerable.GetIndexForwardSubstring(substring, 2);
        }

        /// <summary>
        /// Gibt den numerischen Index des dritten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int ThirdIndexOf<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetIndexForward(3, condition);
        }

        /// <summary>
        /// Gibt den numerischen Index der dritten Position, die den Substring enthält.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="substring">Der gesuchte Substring.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int ThirdIndexOf(this string enumerable, string substring)
        {
            return enumerable.GetIndexForwardSubstring(substring, 3);
        }

        /// <summary>
        /// Gibt den numerischen Index des letzten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.
        /// Wird das gesuchte Element nicht gefunden, wird -1 zurück gegeben.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Numerische Index des Elements, oder -1</returns>
        public static int LastIndexOf<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetIndexBackward(1, condition);
        }

        /// <summary>
        /// Gibt das zweite Element der <paramref name="enumerable"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns>Das zweite Element</returns>
        public static TType Second<TType>(this IEnumerable<TType> enumerable)
        {
            return enumerable.GetElement(2);
        }

        /// <summary>
        /// Gibt das zweite Element der <paramref name="enumerable"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Das zweite Element</returns>
        public static TType Second<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetElement(2, condition);
        }

        /// <summary>
        /// Gibt das zweite Element der <paramref name="enumerable"/> oder den Defaultwert für <typeparamref name="TType"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns>Das zweite Element oder der Defaultwert.</returns>
        public static TType SecondOrDefault<TType>(this IEnumerable<TType> enumerable)
        {
            return enumerable.GetElementOrDefault(2);
        }

        /// <summary>
        /// Gibt das zweite Element der <paramref name="enumerable"/> oder den Defaultwert für <typeparamref name="TType"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Das zweite Element oder der Defaultwert.</returns>
        public static TType SecondOrDefault<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetElementOrDefault(2, condition);
        }

        /// <summary>
        /// Gibt das dritte Element der <paramref name="enumerable"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns>Das dritte Element</returns>
        public static TType Third<TType>(this IEnumerable<TType> enumerable)
        {
            return enumerable.GetElement(3);
        }

        /// <summary>
        /// Gibt das dritte Element der <paramref name="enumerable"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Das dritte Element</returns>
        public static TType Third<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetElement(3, condition);
        }

        /// <summary>
        /// Gibt das dritte Element der <paramref name="enumerable"/> oder den Defaultwert für <typeparamref name="TType"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns>Das dritte Element oder der Defaultwert.</returns>
        public static TType ThirdOrDefault<TType>(this IEnumerable<TType> enumerable)
        {
            return enumerable.GetElementOrDefault(3);
        }

        /// <summary>
        /// Gibt das dritte Element der <paramref name="enumerable"/> oder den Defaultwert für <typeparamref name="TType"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="condition">Eine Bedingung, die Elemente erfüllen müssen, um in dieser Zählung zu gelten.</param>
        /// <returns>Das dritte Element oder der Defaultwert.</returns>
        public static TType ThirdOrDefault<TType>(this IEnumerable<TType> enumerable, Predicate<TType> condition)
        {
            return enumerable.GetElementOrDefault(3, condition);
        }

        private static TType GetElement<TType>(this IEnumerable<TType> enumerable, int offset)
        {
            return enumerable.GetElement(offset, e => true);
        }

        private static TType GetElement<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition)
        {
            if (!enumerable.TryGetElement(offset, condition, out TType element))
            {
                throw new InvalidOperationException("Not enough elements in collection");
            }

            return element;
        }

        private static TType GetElementOrDefault<TType>(this IEnumerable<TType> enumerable, int offset)
        {
            return enumerable.GetElementOrDefault(offset, e => true);
        }

        private static TType GetElementOrDefault<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition)
        {
            enumerable.GetIndexForward(offset, condition, out TType element);
            return element;
        }

        private static bool TryGetElement<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition, out TType element)
        {
            var index = enumerable.GetIndexForward(offset, condition, out element);
            return index != -1;
        }

        private static int GetIndexForward<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition)
        {
            return enumerable.GetIndexForward(offset, condition, out _);
        }

        private static int GetIndexForward<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition, out TType element)
        {
            int index = 0;
            int hits = 0;

            foreach (var e in enumerable)
            {
                if (condition(e) && ++hits == offset)
                {
                    element = e;
                    return index;
                }

                index++;
            }

            element = default;
            return -1;
        }

        private static int GetIndexBackward<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition)
        {
            return enumerable.GetIndexBackward(offset, condition, out _);
        }

        private static int GetIndexBackward<TType>(this IEnumerable<TType> enumerable, int offset, Predicate<TType> condition, out TType element)
        {
            var collection = enumerable.ToArray(); // umgehe multiple Itteration über Enumerable
            var length = collection.Count();
            var index = collection.Reverse().GetIndexForward(offset, condition, out element);
            if (index == -1)
            {
                return index;
            }

            return length - 1 - index;
        }

        private static int GetIndexForwardSubstring(this string str, string substring, int offset)
        {
            int tmpIndex = str.IndexOf(substring, StringComparison.InvariantCulture);
            for (int i = 0; i < offset - 1; i++)
            {
                if (tmpIndex == -1)
                {
                    return -1;
                }
                tmpIndex = str.IndexOf(substring, tmpIndex + 1, StringComparison.InvariantCulture);
            }

            return tmpIndex;
        }
    }
}