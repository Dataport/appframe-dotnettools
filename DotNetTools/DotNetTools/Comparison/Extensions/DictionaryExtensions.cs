using System;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Comparison.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden zum Vergleichen von Dictionaries zur Verfügung.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gibt an, ob das übergebene Dictionary mit dem bestehenden Dictionary vollständig kongruent ist.
        /// </summary>
        /// <typeparam name="TKey">Der Typ des Keys beider Dictionaries</typeparam>
        /// <typeparam name="TValue">Der Wertetyp beider Dictionaries</typeparam>
        /// <param name="value">Das initiale Dictionary</param>
        /// <param name="other">Das zu vergleichende Dictionary</param>
        /// <returns><see langword="true"/> wenn beide Dictionaries kongruent sind, andernfalls <see langword="false"/></returns>
        public static bool IsEquivalentTo<TKey, TValue>(this IDictionary<TKey, TValue> value, IDictionary<TKey, TValue> other)
            where TKey : IComparable
            where TValue : IComparable
        {
            if (value == null && other == null)
            {
                return true;
            }

            if (value == null || other == null)
            {
                return false;
            }

            if (value.Count != other.Count)
            {
                return false;
            }

            if (!value.Keys.OrderBy(k => k).SequenceEqual(other.Keys.OrderBy(k => k)))
            {
                return false;
            }

            foreach (TKey key in value.Keys)
            {
                if (!value[key].Equals(other[key]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gibt an, ob das bestehende Dictionary eine Teilmenge des übergebenen Dictionaries ist.
        /// </summary>
        /// <typeparam name="TKey">Der Typ des Keys beider Dictionaries</typeparam>
        /// <typeparam name="TValue">Der Wertetyp beider Dictionaries</typeparam>
        /// <param name="value">Das initiale Dictionary</param>
        /// <param name="other">Das zu vergleichende Dictionary</param>
        /// <returns><see langword="true"/> wenn das Dictionary eine Teilmenge des übergebenen Dictionaries ist, andernfalls <see langword="false"/></returns>
        public static bool IsSubsetOf<TKey, TValue>(this IDictionary<TKey, TValue> value, IDictionary<TKey, TValue> other)
            where TKey : IComparable
            where TValue : IComparable
        {
            if (value == null && other == null)
            {
                return true;
            }

            if (value == null || other == null)
            {
                return false;
            }

            if (value.Keys.Any(k => !other.Keys.Contains(k)))
            {
                return false;
            }

            foreach (TKey key in value.Keys)
            {
                if (!value[key].Equals(other[key]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}