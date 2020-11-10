using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection
{
    /// <summary>
    /// Tools zum Vergleichen oder Zusammenführen von Werten zweier Objekte.
    /// </summary>
    public static class ObjectComparsionExtensions
    {

        /// <summary>
        /// Vergleicht zwei Objekte anhand der Werte ihrer gemeinsamen Properties.
        /// </summary>
        /// <param name="context">Quelle</param>
        /// <param name="other">Ziel</param>
        /// <param name="ignoreAttribute">Attribute was ggf. Properties vom Vergleich ausschließt (optional)</param>
        /// <returns></returns>
        public static bool IsEqualOnPropertyLevel(this object context, object other, Type ignoreAttribute = null)
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
        /// <returns></returns>
        public static bool IsEqualOnPropertyLevel(this object context, object other, ref MemberComparisonResult[] delta, Type ignoreAttribute = null)
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

        /// <summary>
        /// Verschmilzt zwei Propertys miteinander.
        /// </summary>
        /// <remarks>
        /// Es werden diejenigen Properties überschrieben, die entweder Nothing
        /// sind oder Default-Werte haben.
        /// </remarks>
        public static void MergeWith<T>(this T context, T source)
        {
            foreach (var entry in typeof(T).GetProperties())
            {
                var prop = entry;
                var primaryValue = prop.GetGetMethod().Invoke(context, null);
                var secondaryValue = prop.GetGetMethod().Invoke(source, null);

                // Prüft, ob das Primär-Property entweder Nothing ist, oder ein Value-Type, in dem lediglich der Default-Value steht

                if (primaryValue == null || 
                    (prop.PropertyType.IsValueType && primaryValue.Equals(Activator.CreateInstance(prop.PropertyType))))
                {
                    // Falls die Bedingungen erfüllt sind, wird das Property mit dem Wert des Sekundär-Propertys überschrieben
                    // Im Zweifelsfall kann das natürlich auch Nothing oder ein Default-Value sein; es ist aber sichergestellt, dass niemals
                    // gesetzte Werte des Primär-Propertys überschrieben werden.
                    prop.GetSetMethod().Invoke(context, new[] { secondaryValue });
                }
            }
        }

    }
}

