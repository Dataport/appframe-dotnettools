using System;

namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Extensions für alle Objekttypen.
    /// </summary>
    /// <remarks></remarks>
    public static class ObjectExtensions
    {

        /// <summary>
        /// Löst eine ArgumentNullException aus unter Nennung des Parameternamens wenn 
        /// der Kontext Null/Nothing ist.
        /// Gibt sonst des Objekt unverändert zurück (Fluent).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">Zu prüfendes Objekt.</param>
        /// <param name="paramName">ggf. zu verwendender Parametername für die Exception.</param>
        /// <returns>Zu prüfendes Objekt.</returns>
        /// <remarks></remarks>
        /// <exception cref="ArgumentNullException"></exception> 
        public static T CheckIsNotNull<T>(this T context, string paramName)
        {
            if (context == null)
                throw new ArgumentNullException(paramName);

            return context;
        }

        /// <summary>
        /// Vergleicht mit Equals, wobei sowohl context als auch other null/nothing sein dürfen.
        /// Beide null/nothing gilt als gleich.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool NullSafeEquals<T>(this T context, T other)
        {
            if (context == null & other != null)
                return false;
            if (context != null & other == null)
                return false;
            if (context == null & other == null)
                return true;
            return context.Equals(other);
        }
    }
}
