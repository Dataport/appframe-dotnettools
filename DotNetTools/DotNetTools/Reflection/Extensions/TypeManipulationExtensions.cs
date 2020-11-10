using System;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Stellt Typmanipulations-Methoden zur Verfügung, die implizit logisch sind, aber von .NET nicht automatisch erkannt werden.
    /// </summary>
    public static class TypeManipulationExtensions
    {
        /// <summary>
        /// Definiert den Typ der Enumerable als unterliegenden Typ in der Vererbungshierarchie um.
        /// </summary>
        /// <typeparam name="TCurrent">Der ursprüngliche Typ</typeparam>
        /// <typeparam name="TNew">Der neue Typ</typeparam>
        /// <param name="enumerable">Die zu manipulierende Enumerable.</param>
        /// <returns>Die umdefinierte Enumerable</returns>
        public static IEnumerable<TNew> DowngradeType<TCurrent, TNew>(this IEnumerable<TCurrent> enumerable)
            where TCurrent : TNew
        {
            return enumerable.Select(e => (TNew)e);
        }

        /// <summary>
        /// Definiert den Typ der Enumerable als oberliegenden Typ in der Vererbungshierarchie um.
        /// </summary>
        /// <typeparam name="TCurrent">Der ursprüngliche Typ</typeparam>
        /// <typeparam name="TNew">Der neue Typ</typeparam>
        /// <param name="enumerable">Die zu manipulierende Enumerable.</param>
        /// <returns>Die umdefinierte Enumerable</returns>
        /// <exception cref="InvalidCastException">Das Upgrade war aufgrund von Typinkompatibilität nicht möglich.</exception>
        public static IEnumerable<TNew> UpgradeType<TCurrent, TNew>(this IEnumerable<TCurrent> enumerable)
            where TNew : TCurrent
        {
            return enumerable.Select(e => (TNew)e);
        }

        /// <summary>
        /// Definiert den Typ der Schlüsselwerte des Dictionaries als unterliegenden Typ in der Vererbungshierarchie um.
        /// </summary>
        /// <typeparam name="TKeyCurrent">Der ursprüngliche Typ der Schlüsselwerte</typeparam>
        /// <typeparam name="TKeyNew">Der neue Typ der Schlüsselwerte</typeparam>
        /// <typeparam name="TValue">Der Typ der Werte</typeparam>
        /// <param name="dictionary">Das zu manipulierende Dictionary.</param>
        /// <returns>Das umdefinierte Dictionary</returns>
        public static IDictionary<TKeyNew, TValue> DowngradeKeyType<TKeyCurrent, TKeyNew, TValue>(this IDictionary<TKeyCurrent, TValue> dictionary)
            where TKeyCurrent : TKeyNew
        {
            return dictionary.ToDictionary(d => (TKeyNew)d.Key, d => d.Value);
        }

        /// <summary>
        /// Definiert den Typ der Schlüsselwerte des Dictionaries als oberliegenden Typ in der Vererbungshierarchie um.
        /// </summary>
        /// <typeparam name="TKeyCurrent">Der ursprüngliche Typ der Schlüsselwerte</typeparam>
        /// <typeparam name="TKeyNew">Der neue Typ der Schlüsselwerte</typeparam>
        /// <typeparam name="TValue">Der Typ der Werte</typeparam>
        /// <param name="dictionary">Das zu manipulierende Dictionary.</param>
        /// <returns>Das umdefinierte Dictionary</returns>
        /// <exception cref="InvalidCastException">Das Upgrade war aufgrund von Typinkompatibilität nicht möglich.</exception>
        public static IDictionary<TKeyNew, TValue> UpgradeKeyType<TKeyCurrent, TKeyNew, TValue>(this IDictionary<TKeyCurrent, TValue> dictionary)
            where TKeyNew : TKeyCurrent
        {
            return dictionary.ToDictionary(d => (TKeyNew)d.Key, d => d.Value);
        }

        /// <summary>
        /// Definiert den Typ der Werte des Dictionaries als unterliegenden Typ in der Vererbungshierarchie um.
        /// </summary>
        /// <typeparam name="TKey">Der Typ der Schlüsselwerte</typeparam>
        /// <typeparam name="TValueCurrent">Der ursprüngliche Typ der Werte</typeparam>
        /// <typeparam name="TValueNew">Der neue Typ der Werte</typeparam>
        /// <param name="dictionary">Das zu manipulierende Dictionary</param>
        /// <returns>Das umdefinierte Dictionary</returns>
        public static IDictionary<TKey, TValueNew> DowngradeValueType<TKey, TValueCurrent, TValueNew>(this IDictionary<TKey, TValueCurrent> dictionary)
            where TValueCurrent : TValueNew
        {
            return dictionary.ToDictionary(d => d.Key, d => (TValueNew)d.Value);
        }

        /// <summary>
        /// Definiert den Typ der Werte des Dictionaries als oberliegenden Typ in der Vererbungshierarchie um.
        /// </summary>
        /// <typeparam name="TKey">Der Typ der Schlüsselwerte</typeparam>
        /// <typeparam name="TValueCurrent">Der ursprüngliche Typ der Werte</typeparam>
        /// <typeparam name="TValueNew">Der neue Typ der Werte</typeparam>
        /// <param name="dictionary">Das zu manipulierende Dictionary</param>
        /// <returns>Das umdefinierte Dictionary</returns>
        /// <exception cref="InvalidCastException">Das Upgrade war aufgrund von Typinkompatibilität nicht möglich.</exception>
        public static IDictionary<TKey, TValueNew> UpgradeValueType<TKey, TValueCurrent, TValueNew>(this IDictionary<TKey, TValueCurrent> dictionary)
            where TValueNew : TValueCurrent
        {
            return dictionary.ToDictionary(d => d.Key, d => (TValueNew)d.Value);
        }
    }
}