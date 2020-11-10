using Dataport.AppFrameDotNet.DotNetTools.Validation.Models;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Validation.Extensions
{
    /// <summary>
    /// Erweitert die <see cref="Condition{T}"/>-Klasse um Prüfmethoden.
    /// </summary>
    public static class ConditionExtensions
    {
        /// <summary>
        /// Stellt sicher, dass das <see cref="Condition{T}.ObjectToVerify"/> nicht <see langword="null"/> ist.
        /// </summary>
        /// <typeparam name="T">Der Typ des Prüfobjekts</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentNullException">Das <see cref="Condition{T}.ObjectToVerify"/> ist <see langword="null"/>.</exception>
        public static Condition<T> IsNotNull<T>(this Condition<T> condition)
        {
            if (condition.ObjectToVerify == null)
            {
                throw new ArgumentNullException(condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass eine Bedingung erfüllt ist.
        /// </summary>
        /// <typeparam name="T">Der Typ des Prüfobjekts</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <param name="predicate">Die Bedingung die erfüllt sein muss.</param>
        /// <param name="message">Eine zusätzliche Nachricht, die ausgegeben werden soll.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentException">Die Bedingung wurde nicht erfüllt.</exception>
        [SuppressMessage("ReSharper", "ArgumentExceptionConstructorArgument", Justification = "Ist ein Bug.")]
        public static Condition<T> Is<T>(this Condition<T> condition, Func<T, bool> predicate, string message = null)
        {
            if (predicate == null)
            {
                // TODO: Change to ArgumentNullException in Version 4 (Annotation entfernen)
                throw new ArgumentException(nameof(predicate));
            }

            if (!predicate(condition.ObjectToVerify))
            {
                if (message == null)
                {
                    throw new ArgumentException(condition.NameOfObject);
                }

                throw new ArgumentException(message, condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass das Objekt nicht <see langword="null"/> und nicht leer ist.
        /// </summary>
        /// <typeparam name="T">Der Typ des Prüfobjekts</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentException">Der Objekt ist <see langword="null"/> leer.</exception>
        public static Condition<T> IsNotNullOrEmpty<T>(this Condition<T> condition) where T : IEnumerable
        {
            if (condition.ObjectToVerify == null)
            {
                throw new ArgumentNullException($"{condition.NameOfObject} must be present", condition.NameOfObject);
            }

            return condition.ContainsElements();
        }

        /// <summary>
        /// Stellt sicher, dass das Objekt nicht <see langword="null"/> und nicht leer ist und nicht nur leere Zeichen enthält.
        /// </summary>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentException">Der Objekt ist <see langword="null"/>, leer oder enthält nur leere Zeichen.</exception>
        public static Condition<string> IsNotNullOrWhiteSpace(this Condition<string> condition)
        {
            if (string.IsNullOrWhiteSpace(condition.ObjectToVerify))
            {
                throw new ArgumentException($"{condition.NameOfObject} is null or whitespace.", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass die Instanz Elemente hat.
        /// </summary>
        /// <typeparam name="T">Der Typ des Prüfobjekts</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentException">Die Instanz enthält keine Elemente.</exception>
        public static Condition<T> ContainsElements<T>(this Condition<T> condition) where T : IEnumerable
        {
            if (!condition.ObjectToVerify.Cast<object>().Any())
            {
                throw new ArgumentException($"{condition.NameOfObject} is an empty enumeration.", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass der Enum-Member definiert ist.
        /// </summary>
        /// <typeparam name="T">Der Typ des Enums</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentException">Die Objekt ist nicht definiert.</exception>
        public static Condition<T> IsDefined<T>(this Condition<T> condition) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), condition.ObjectToVerify))
            {
                throw new ArgumentException($"{condition.NameOfObject} is not defined for type {typeof(T)}", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass die Instanz vergleichbar zu einem Vergleichswert ist.
        /// Verhalten: Gibt <see langword="true"/> zurück, wenn beide Werte <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Der Typ des Prüfobjekts</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <param name="compare">Der Vergleichswert</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentException">Die Objekte sind nicht identisch.</exception>
        public static Condition<T> IsEqualTo<T>(this Condition<T> condition, T compare)
        {
            if (condition.ObjectToVerify == null && compare == null)
            {
                return condition;
            }

            if (condition.ObjectToVerify == null || compare == null || !condition.ObjectToVerify.Equals(compare))
            {
                throw new ArgumentException($"{condition.ObjectToVerify} is not equal to {compare}", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass alle Werte in der übergebenen Collection nicht <see langword="null"/> sind.
        /// </summary>
        /// <typeparam name="T">Der Typ der Enumeration.</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentNullException">Die Collection war null.</exception>
        /// <exception cref="ArgumentException">Mindestens ein Wert in der Collection war null.</exception>
        public static Condition<T> AllElementsNotNull<T>(this Condition<T> condition) where T : IEnumerable
        {
            if (condition.ObjectToVerify == null)
            {
                throw new ArgumentNullException(condition.NameOfObject);
            }

            if (condition.ObjectToVerify.Cast<object>().Any(o => o == null))
            {
                throw new ArgumentException($"{condition.NameOfObject} contains elements with null-values.", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass die übergebene Collection eine exakte Anzahl an Elementen enthält.
        /// </summary>
        /// <typeparam name="T">Der Typ der Enumeration.</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <param name="count">Die erwartete Anzahl der Elemente</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentNullException">Die Collection war null.</exception>
        /// <exception cref="ArgumentException">Die Enumeration enthält nicht die exakte Anzahl an Werten.</exception>
        public static Condition<T> HasCount<T>(this Condition<T> condition, int count) where T : IEnumerable
        {
            if (condition.ObjectToVerify == null)
            {
                throw new ArgumentNullException(condition.NameOfObject);
            }

            var length = condition.ObjectToVerify.Cast<object>().Count();
            if (length != count)
            {
                throw new ArgumentException($"{condition.NameOfObject} should have count {count} but had {length}", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass die übergebene Collection eine Mindestanzahl an Elementen enthält.
        /// </summary>
        /// <typeparam name="T">Der Typ der Enumeration.</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <param name="size">Die erwartete minimale Anzahl der Elemente</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentNullException">Die Collection war null.</exception>
        /// <exception cref="ArgumentException">Die Enumeration enthält nicht die minimale Anzahl an Werten.</exception>
        public static Condition<T> HasMinimumSize<T>(this Condition<T> condition, int size) where T : IEnumerable
        {
            if (condition.ObjectToVerify == null)
            {
                throw new ArgumentNullException(condition.NameOfObject);
            }

            var length = condition.ObjectToVerify.Cast<object>().Count();
            if (length < size)
            {
                // TODO: Change to ArgumentOutOfRange-Exception in Version 4
                throw new ArgumentException($"{condition.NameOfObject} should at least have {size} elements but had {length}", condition.NameOfObject);
            }

            return condition;
        }

        /// <summary>
        /// Stellt sicher, dass die übergebene Collection eine Maximalanzahl an Elementen enthält.
        /// </summary>
        /// <typeparam name="T">Der Typ der Enumeration.</typeparam>
        /// <param name="condition">Die Instanz mit allen Überprüfungs-Informationen.</param>
        /// <param name="size">Die erwartete maximale Anzahl der Elemente</param>
        /// <returns>Eine überprüfbare Bedingung</returns>
        /// <exception cref="ArgumentNullException">Die Collection war null.</exception>
        /// <exception cref="ArgumentException">Die Enumeration enthält mehr als die maximale Anzahl an Werten.</exception>
        public static Condition<T> HasMaximumSize<T>(this Condition<T> condition, int size) where T : IEnumerable
        {
            if (condition.ObjectToVerify == null)
            {
                throw new ArgumentNullException(condition.NameOfObject);
            }

            var length = condition.ObjectToVerify.Cast<object>().Count();
            if (length > size)
            {
                // TODO: Change to ArgumentOutOfRange-Exception in Version 4
                throw new ArgumentException($"{condition.NameOfObject} shouldn't have more than {size} elements but had {length}", condition.NameOfObject);
            }

            return condition;
        }
    }
}