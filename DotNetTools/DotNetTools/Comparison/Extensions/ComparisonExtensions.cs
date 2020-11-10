using Dataport.AppFrameDotNet.DotNetTools.Comparison.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Comparison.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Vergleichen von Werten zur Verfügung.
    /// </summary>
    public static class ComparisonExtensions
    {
        /// <summary>
        /// Gibt an ob der Wert innerhalb eines bestimmten Wertebereichs liegt.
        /// </summary>
        /// <param name="value">Der Wert der zu prüfen ist</param>
        /// <param name="lowerBoundary">Der untere Grenzwert</param>
        /// <param name="upperBoundary">Der obere Grenzwert</param>
        /// <param name="boundaryType">Eine Vorschrift wie mit den Grenzwerten umzugehen ist</param>
        /// <returns><see langword="true"/> wenn der Wert innerhalb eines Wertebereichs ist, andernfalls <see langword="false"/></returns>
        public static bool IsInRange(this IComparable value, IComparable lowerBoundary, IComparable upperBoundary, BoundaryType boundaryType = BoundaryType.Inclusive)
        {
            if (lowerBoundary != null && lowerBoundary.CompareTo(upperBoundary) > 0)
            {
                throw new ArgumentException("The lower boundary must be lower oder equal the upper boundary");
            }

            switch (boundaryType)
            {
                case BoundaryType.Exclusive:
                    return value.CompareTo(lowerBoundary) > 0 && value.CompareTo(upperBoundary) < 0;

                case BoundaryType.Inclusive:
                    return value.CompareTo(lowerBoundary) >= 0 && value.CompareTo(upperBoundary) <= 0;

                case BoundaryType.LowerOnly:
                    return value.CompareTo(lowerBoundary) >= 0 && value.CompareTo(upperBoundary) < 0;

                case BoundaryType.UpperOnly:
                    return value.CompareTo(lowerBoundary) > 0 && value.CompareTo(upperBoundary) <= 0;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Gibt an ob sich der Wert im Rahmen eines Maximalwerts befindet.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Werts</typeparam>
        /// <param name="value">Der Wert der zu prüfen ist</param>
        /// <param name="withinValue">Der obere Grenzwert</param>
        /// <param name="boundaryType">Eine Vorschrift wie mit den Grenzwerten umzugehen ist</param>
        /// <returns><see langword="true"/> wenn der Wert innerhalb eines Wertebereichs ist, andernfalls <see langword="false"/></returns>
        public static bool IsWithin<TType>(this TType value, TType withinValue, BoundaryType boundaryType = BoundaryType.Inclusive) where TType : IComparable
        {
            return value.IsInRange(null, withinValue, boundaryType);
        }

        /// <summary>
        /// Vergleicht zwei Objekte anhand der Werte ihrer gemeinsamen Properties.
        /// </summary>
        /// <param name="context">Quelle</param>
        /// <param name="other">Ziel</param>
        /// <param name="ignoreAttribute">Attribute was ggf. Properties vom Vergleich ausschließt (optional)</param>
        /// <returns><see langword="true"/> wenn die Objekte übereinstimmen, andernfalls <see langword="false"/></returns>
        public static bool IsEqualOnPropertyLevel<TType1, TType2>(this TType1 context, TType2 other, Type ignoreAttribute = null)
            where TType1 : class
            where TType2 : class
        {
            MemberComparisonResult[] dummy = null;
            return IsEqualOnPropertyLevelInternal(context, other, ignoreAttribute, true, ref dummy);
        }

        /// <summary>
        /// Vergleicht zwei Objekte anhand der Werte ihrer gemeinsamen Properties.
        /// </summary>
        /// <param name="context">Quelle</param>
        /// <param name="other">Ziel</param>
        /// <param name="delta">Abweichungen</param>
        /// <param name="ignoreAttribute">Attribute was ggf. Properties vom Vergleich ausschließt (optional)</param>
        /// <returns><see langword="true"/> wenn die Objekte übereinstimmen, andernfalls <see langword="false"/></returns>
        public static bool IsEqualOnPropertyLevel<TType1, TType2>(this TType1 context, TType2 other, ref MemberComparisonResult[] delta, Type ignoreAttribute = null)
            where TType1 : class
            where TType2 : class
        {
            return IsEqualOnPropertyLevelInternal(context, other, ignoreAttribute, false, ref delta);
        }

        private static bool IsEqualOnPropertyLevelInternal(object source, object target, Type ignoreAttribute, bool quickmode, ref MemberComparisonResult[] delta)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (ignoreAttribute != null && !ignoreAttribute.IsSubclassOf(typeof(Attribute)))
            {
                throw new ArgumentOutOfRangeException(nameof(ignoreAttribute), "Angegebener Typ muss ein Attribut sein");
            }

            var deltaList = new List<MemberComparisonResult>();

            //Durch alle möglichen Zielfelder iterieren
            var mi = target.GetType().GetProperties().Where(x => !x.GetIndexParameters().Any()).ToArray();
            foreach (var member in mi)
            {
                var targetProperty = member;
                var sourceProperty = source.GetType().GetProperty(member.Name);

                //Gibt es ein gleichnamiges Quellfeld das nicht mit dem IgnoreAttribute versehen ist, dann auswerten
                if (sourceProperty != null && (ignoreAttribute == null ||
                    source.GetType().GetMember(member.Name)[0].GetCustomAttribute(ignoreAttribute, true) == null))
                {
                    var sourceValue = sourceProperty.GetValue(source, null);
                    var targetValue = targetProperty.GetValue(target, null);

                    bool isEqual;

                    if (sourceValue == null)
                    {
                        isEqual = (targetValue == null);
                    }
                    else
                    {
                        isEqual = sourceValue.Equals(targetValue);
                    }

                    if (isEqual) continue;

                    if (quickmode)
                    {
                        delta = null;
                        return false;
                    }

                    deltaList.Add(new MemberComparisonResult(member.Name, sourceProperty, targetProperty, sourceValue, targetValue));
                }
            }

            delta = deltaList.ToArray();
            return !deltaList.Any();
        }
    }
}