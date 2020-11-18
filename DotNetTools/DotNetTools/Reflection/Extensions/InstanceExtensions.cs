using System;
using System.Diagnostics.CodeAnalysis;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Erweitert Klassen um Lese-, Transformations- und Schreibzugriffsmethoden.
    /// </summary>
    public static class InstanceExtensions
    {
        /// <summary>
        /// Gibt den angeforderten Wert zurück.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <typeparam name="TProperty">Der Typ des Resultats</typeparam>
        /// <param name="instance">Die Instanz</param>
        /// <param name="getter">Vorschrift zum Auslesen des Werts.</param>
        /// <returns>Der ausgelesene Wert.</returns>
        /// <exception cref="NullReferenceException">Die Instanz ist <see langword="null"/></exception>
        [SuppressMessage("ReSharper", "UnthrowableException", Justification = "Das ist ein Bug")]
        public static TProperty Get<TInstance, TProperty>(this TInstance instance, Func<TInstance, TProperty> getter)
            where TInstance : class
        {
            if (instance == null)
            {
                var name = typeof(TInstance).Name;
                // TODO: Change To ArgumentNullException in Version 4
                throw new NullReferenceException($"{name}: Object reference not set to an instance of an object.");
            }

            return getter(instance);
        }

        /// <summary>
        /// Setzt einen Wert.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <param name="instance">Die Instanz</param>
        /// <param name="setter">Eine Vorschrift, wie ein Wert zu setzten ist.</param>
        /// <exception cref="NullReferenceException">Die Instanz ist <see langword="null"/></exception>
        [SuppressMessage("ReSharper", "UnthrowableException", Justification = "Das ist ein Bug")]
        public static void Set<TInstance>(this TInstance instance, Action<TInstance> setter)
            where TInstance : class
        {
            if (instance == null)
            {
                var name = typeof(TInstance).Name;
                // TODO: Change To ArgumentNullException in Version 4
                throw new NullReferenceException($"{name}: Object reference not set to an instance of an object.");
            }

            setter(instance);
        }

        /// <summary>
        /// Verschmilzt mehrere Propertys zweier Instanzen miteinander.
        /// </summary>
        /// <param name="instance">Die Instanz die modifiziert wird.</param>
        /// <param name="source">Die Instanz die als Referenz benutzt wird.</param>
        /// <remarks>
        /// Es werden diejenigen Properties überschrieben, die entweder Nothing
        /// sind oder Default-Werte haben.
        /// </remarks>
        public static void MergeWith<TInstance>(this TInstance instance, TInstance source) where TInstance : class
        {
            foreach (var entry in typeof(TInstance).GetProperties())
            {
                var prop = entry;
                var primaryValue = prop.GetGetMethod()?.Invoke(instance, null);
                var secondaryValue = prop.GetGetMethod()?.Invoke(source, null);

                // Prüft, ob das Primär-Property entweder Nothing ist, oder ein Value-Type, in dem lediglich der Default-Value steht

                if (primaryValue == null ||
                    (prop.PropertyType.IsValueType && primaryValue.Equals(Activator.CreateInstance(prop.PropertyType))))
                {
                    // Falls die Bedingungen erfüllt sind, wird das Property mit dem Wert des Sekundär-Propertys überschrieben
                    // Im Zweifelsfall kann das natürlich auch Nothing oder ein Default-Value sein; es ist aber sichergestellt, dass niemals
                    // gesetzte Werte des Primär-Propertys überschrieben werden.
                    prop.GetSetMethod()?.Invoke(instance, new[] { secondaryValue });
                }
            }
        }
    }
}