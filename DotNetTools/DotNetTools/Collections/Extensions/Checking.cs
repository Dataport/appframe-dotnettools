using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Stellt Überprüfungsmethoden zur Verfügung
    /// </summary>
    public static class Checking
    {
        /// <summary>
        /// Gibt an, ob die Enumeration <see langword="null"/> oder leer ist.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns><see langword="true"/>, wenn die Enumeration <see langword="null"/> oder leer ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsNullOrEmpty<TType>(this IEnumerable<TType> enumerable)
        {
            return enumerable == null || enumerable.IsEmpty();
        }

        /// <summary>
        /// Gibt an, ob die Enumeration leer ist.
        /// Verhalten: Gibt false zurück, wenn die Enumeration <see langword="null"/> ist.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns><see langword="true"/>, wenn die Enumeration leer ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsEmpty<TType>(this IEnumerable<TType> enumerable)
        {
            return false == enumerable?.Any();
        }

        /// <summary>
        /// Gibt an, ob die Enumeration einen oder mehrere der übergebenen <paramref name="containCandidates"/> enthält.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="containCandidates">Die Elemente von denen eines enthalten sein muss.</param>
        /// <returns><see langword="true"/>, wenn die Enumeration ein oder mehrere Elemente enthält, andernfalls <see langword="false"/>.</returns>
        public static bool ContainOneOf<TType>(this IEnumerable<TType> enumerable, params TType[] containCandidates)
        {
            if (enumerable == null)
            {
                return false;
            }

            return enumerable.Any(e => containCandidates.Contains(e));
        }

        /// <summary>
        /// Gibt an, ob die Enumeration alle der übergebenen <paramref name="containCandidates"/> enthält.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="containCandidates">Die Elemente alle enthalten sein müssen.</param>
        /// <returns><see langword="true"/>, wenn die Enumeration alle Elemente enthält, andernfalls <see langword="false"/>.</returns>
        public static bool ContainAll<TType>(this IEnumerable<TType> enumerable, params TType[] containCandidates)
        {
            if (enumerable == null)
            {
                return false;
            }

            var numberOfElements = containCandidates.Length;
            var hits = 0;

            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext() && numberOfElements != hits)
                {
                    if (containCandidates.Contains(enumerator.Current))
                    {
                        hits++;
                    }
                }
            }

            return numberOfElements == hits;
        }
    }
}