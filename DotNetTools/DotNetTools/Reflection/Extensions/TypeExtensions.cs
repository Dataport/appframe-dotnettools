using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für <see cref="Type"/> zur Verfügung.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gibt an, ob der Datentyp nullable ist.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <returns><see langword="true"/> wenn der Typ nullable ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsNullable(this Type type)
        {
            return
                type.IsClass ||
                type.IsInterface ||
                Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Gibt an, dass der Typ einen anderen Typen implementiert.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="implementationType">Der Typ der implementiert sein soll.</param>
        /// <returns><see langword="true"/> wenn der Typ implementiert ist, andernfalls <see langword="false"/>.</returns>
        public static bool Implements(this Type type, Type implementationType)
        {
            return implementationType.IsInterface
                && implementationType.IsAssignableFrom(type.GetTypeInfo());
        }

        /// <summary>
        /// Gibt an, dass der Typ von einem anderen Typen ableitet.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="extensionType">Der Typ von dem abgeleitet sein soll.</param>
        /// <returns><see langword="true"/> wenn von dem Typ abgeleitet ist, andernfalls <see langword="false"/>.</returns>
        public static bool Extends(this Type type, Type extensionType)
        {
            return extensionType.IsClass
                && extensionType.IsAssignableFrom(type.GetTypeInfo());
        }

        /// <summary>
        /// Gibt an, ob der Typ generisch ist und der übergebenen Definition entspricht, oder nicht.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="definition">Die Definition die der Typ erfüllen muss.</param>
        /// <returns><see langword="true"/> wenn der Typ generisch ist und der übergebenen Definition entspricht, andernfalls <see langword="false"/>.</returns>
        public static bool IsGenericType(this Type type, Type definition)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == definition || type == definition);
        }

        /// <summary>
        /// Gibt an, ob der Typ eine generische <see cref="ICollection{T}"/> ist oder nicht.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <returns><see langword="true"/> wenn der Typ eine generische <see cref="ICollection{T}"/> ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsGenericCollection(this Type type)
        {
            return type.IsGenericTypeOf(typeof(ICollection<>));
        }

        /// <summary>
        /// Gibt an, ob der Typ eine generische <see cref="IEnumerable{T}"/> ist oder nicht.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <returns><see langword="true"/> wenn der Typ eine generische <see cref="IEnumerable{T}"/> ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsGenericEnumerable(this Type type)
        {
            return type.IsGenericTypeOf(typeof(IEnumerable<>));
        }

        /// <summary>
        /// Gibt an, ob der Typ den übergebenen generischen Typ implementiert bzw. von ihm ableitet.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="ofType">Der Typ der implementiert sein muss.</param>
        /// <returns><see langword="true"/> wenn der Typ implementiert ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsGenericTypeOf(this Type type, Type ofType)
        {
            return type.GetInterfaces().Any(t => t.IsGenericType(ofType))
                || (type.BaseType?.IsGenericType(ofType) ?? false);
        }

        /// <summary>
        /// Gibt an, ob der Typ generisches ist und der übergebenen Typen diesen implementiert bzw. von ihm ableitet.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="forType">Der Typ der implementiert sein muss.</param>
        /// <returns><see langword="true"/> wenn der Typ implementiert ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsGenericTypeFor(this Type type, Type forType)
        {
            return type.IsGenericType && forType.IsGenericTypeOf(type);
        }

        /// <summary>
        /// Gibt das Property anhand seines Namens zurück.
        /// Verhalten: Mit dieser Methoden können im Gegensatz zur klassischen GetProperty-Methode auch nicht öffentliche Member abgerufen werden.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="propertyName">Der Name des Properties.</param>
        /// <returns>Die Informationen des Properties</returns>
        /// <exception cref="ArgumentException">Es existiert kein Property mit diesem Namen.</exception>
        public static PropertyInfo GetPropertyByName(this Type type, string propertyName)
        {
            var propertyInfo = type.GetTypeInfo().GetDeclaredProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Unknown property '{propertyName}' for type '{type}'");
            }

            return propertyInfo;
        }

        /// <summary>
        /// Gibt an, ob der Typ in seiner Vererbungskette den Vergleichstyp enthält.
        /// </summary>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="compare">Der Typ mit dem verglichen wird.</param>
        /// <returns><see langword="true"/> wenn der Typ in der Vererbungskette enthalten ist, andernfalls <see langword="false"/>.</returns>
        public static bool IsOrIsInheritFrom(this Type type, Type compare)
        {
            if (type == compare || type.Extends(compare) || type.Implements(compare))
            {
                return true;
            }

            if (type == typeof(object) || type.BaseType == null)
            {
                return false;
            }

            return type.BaseType.IsOrIsInheritFrom(compare);
        }

        /// <summary>
        /// Gibt einen öffentlich sichtbaren statischen oder konstanten Member-Wert zurück.
        /// </summary>
        /// <typeparam name="TValue">Der Typ des Wertes</typeparam>
        /// <param name="type">Der Typ der zu prüfen ist.</param>
        /// <param name="memberName">Der Name des Members.</param>
        /// <returns>Der Wert des Members</returns>
        /// <exception cref="ArgumentException">Es existiert kein Member mit dem übergebenen Namen.</exception>
        public static TValue GetConstantValue<TValue>(this Type type, string memberName)
        {
            var field = type.GetField(memberName);
            if (field != null)
            {
                return (TValue)field.GetValue(null);
            }

            var property = type.GetProperty(memberName);
            if (property != null)
            {
                return (TValue)property.GetValue(null);
            }

            throw new ArgumentException($"There is no property or field with name '{memberName}' in Typ '{type}'");
        }
    }
}