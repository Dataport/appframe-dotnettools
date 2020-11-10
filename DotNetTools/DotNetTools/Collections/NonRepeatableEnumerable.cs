using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections
{
    /// <summary>
    /// Stellt eine Enumeration dar, welche nur ein einziges Mal evaluiert werden kann.
    /// </summary>
    /// <typeparam name="TType">Der Typ der Enumeration</typeparam>
    public class NonRepeatableEnumerable<TType> : IEnumerable<TType>
    {
        private readonly TType[] _collection;

        private int _enumerationCounter;

        /// <summary>
        /// Erzeugt die Enumeration
        /// </summary>
        /// <param name="collection">Die innere Collection</param>
        public NonRepeatableEnumerable(IEnumerable<TType> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            _collection = collection.ToArray();
        }

        /// <summary>Gibt einen Enumerator zurück, der durch die Collection itteriert.</summary>
        /// <returns>Der Enumerator.</returns>
        public IEnumerator<TType> GetEnumerator()
        {
            int length = _collection.Length;
            while (_enumerationCounter < length)
            {
                yield return _collection[_enumerationCounter++];
            }
        }

        /// <summary>Gibt einen Enumerator zurück, der durch die Collection itteriert.</summary>
        /// <returns>Der Enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}