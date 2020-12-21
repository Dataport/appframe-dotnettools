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
        private static readonly Dictionary<Type, TypeAlias> TypeAliases = new[]
        {
            new TypeAlias(typeof(byte)),
            new TypeAlias(typeof(sbyte)),
            new TypeAlias(typeof(short), "short"),
            new TypeAlias(typeof(ushort), "ushort"),
            new TypeAlias(typeof(int), "int"),
            new TypeAlias(typeof(uint), "uint"),
            new TypeAlias(typeof(long), "long"),
            new TypeAlias(typeof(ulong), "ulong"),
            new TypeAlias(typeof(float), "float"),
            new TypeAlias(typeof(double)),
            new TypeAlias(typeof(decimal)),
            new TypeAlias(typeof(object)),
            new TypeAlias(typeof(bool), "bool"),
            new TypeAlias(typeof(char)),
            new TypeAlias(typeof(string)),
            new TypeAlias(typeof(void))
        }.ToDictionary(l => l.Type);

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

        /// <summary>
        /// Gibt den lesbaren Namen des Typs (CodeName) zurück.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="includeConstraints">Gibt an, ob bei der Ausgabe auch die Constraints von generischen Parametern ausgegeben werden sollen.</param>
        /// <param name="useBuildInNames">Gibt an, dass die von .Net standardisierten Namen verwendet werden.</param>
        /// <returns>Lesbarer Name des Typs mit generischen Argumenten/Parametern.</returns>
        public static string GetCodeName(this Type type, bool includeConstraints = false, bool useBuildInNames = false)
        {
            var parentType = type.DeclaringType;

            bool IsArrayType(out Type elementType)
            {
                elementType = type.IsArray ? type.GetElementType() : null;
                return elementType != null;
            }

            if (IsArrayType(out var arrayElementType))
            {
                // "arrayElementType.GetCodeName" ==> notwendig für...
                // - useBuildInNames = false (ggf. über Alias bestimmen)
                // - arrayElementType.IsGeneric = true (Generische Typen müssen korrekt aufgelöst werden)
                var arraySuffix = $"[{string.Join(",", new string[type.GetArrayRank()])}]";
                return $"{arrayElementType.GetCodeName(includeConstraints, useBuildInNames)}{arraySuffix}";
            }

            bool IsNullableValueType(out Type underlyingType)
            {
                underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType != null;
            }

            if (!useBuildInNames && IsNullableValueType(out var valueType))
            {
                return $"{valueType.GetCodeName(includeConstraints)}?"; // add "nullable" compiler symbol '?'
            }

            var typeName = !useBuildInNames && TypeAliases.ContainsKey(type) ? TypeAliases[type].Alias : type.Name;

            var genericSuffixIndex = typeName.IndexOf('`');

            if (type.IsGenericType)
            {
                var genericTypeArguments = type.GetGenericTypeArgumentOrParameter();
                var genericArgumentCounter = genericTypeArguments.Length;

                if (type.IsNested)
                {
                    // Ist der ParentType (DeclaringType) ein GenericType?
                    if (parentType?.IsGenericType ?? false)
                    {
                        // Wie hoch ist der Counter für generische Argumente bei "diesem" Type?
                        // Beispiel:
                        //      MasterClass<TParameter, TValue>.ChildClass<TType>
                        //      typeName = "ChildClass`1" == also ==> Counter = 1
                        if (genericSuffixIndex >= 0)
                        {
                            var genericArgumentCounterIndex = genericSuffixIndex + 1;
                            genericArgumentCounter = int.Parse(typeName.Substring(genericArgumentCounterIndex, typeName.Length - genericArgumentCounterIndex));
                        }
                        else genericArgumentCounter = 0;

                        // Wenn ein Missverhältnis bei den Argumenten besteht,
                        // dann müssen die Generischen Argumente manuell ermittelt werden.
                        // Beispiel:
                        //      MasterClass<TParameter, TValue>.ChildClass<TType>
                        //      Counter = 1 (siehe oben)
                        //      GenericTypeArguments = 3 ('MasterClass' wird mitgezählt)
                        if (genericArgumentCounter != genericTypeArguments.Length)
                        {
                            var skipCounter = genericTypeArguments.Length - genericArgumentCounter;
                            parentType = parentType.MakeGenericType(genericTypeArguments.Take(skipCounter).ToArray());
                            genericTypeArguments = genericTypeArguments.Skip(skipCounter).ToArray();
                        }
                    }
                }

                // Wenn es für "diesen" Type generische Argumente gibt,
                // dann einen lesbaren Namen mit diesen Argumenten bilden.
                if (genericArgumentCounter > 0)
                {
                    if (genericSuffixIndex >= 0) typeName = typeName.Substring(0, genericSuffixIndex);
                    var genericTypeArgumentCodeNames = genericTypeArguments.Select(t => t.GetCodeName(includeConstraints, useBuildInNames));
                    typeName = $"{typeName}<{string.Join(", ", genericTypeArgumentCodeNames)}>";
                }
            }

            if (type.IsGenericParameter)
            {
                if (!includeConstraints) return typeName;

                var constraintCodeNames = type.GetGenericParameterConstraintCodeNames(useBuildInNames);
                if (constraintCodeNames.Length > 0)
                    typeName = $"{{{typeName} : {string.Join(", ", constraintCodeNames)}}}";

                if (type.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Covariant))
                    typeName = $"out {typeName}";
                if (type.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant))
                    typeName = $"in {typeName}";
            }
            else if (parentType != null) // ParentType nur anfügen, wenn es "kein" GenericParameter ist
                typeName = $"{parentType.GetCodeName(includeConstraints, useBuildInNames)}.{typeName}";

            return typeName;
        }

        private static string[] GetGenericParameterConstraintCodeNames(this Type genericParameter, bool useBuildInNames)
        {
            var constraints = genericParameter.GetGenericParameterConstraints();
            var constraintCodeNames = constraints.Select(c => c.GetCodeName(true, useBuildInNames));
            if (genericParameter.GenericParameterAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
                constraintCodeNames = constraintCodeNames.Prepend("class");
            if (genericParameter.GenericParameterAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
                constraintCodeNames = constraintCodeNames.Append("new()");

            return constraintCodeNames.ToArray();
        }

        private static Type[] GetGenericTypeArgumentOrParameter(this Type type)
        {
            // IsConstructedGenericType ==> Action<string, int> (Beispiel)
            // IsNotConstructedGenericType ==> Action<,> (Beispiel)
            var genericTypeArguments = type.IsConstructedGenericType
                ? type.GenericTypeArguments     // definierte Typen bei einem konstruierten Generic
                : type.GetGenericArguments();   // Parameter für einen nicht konstruierten Generic

            return genericTypeArguments;
        }

        private class TypeAlias
        {
            public Type Type { get; }

            public string Alias { get; }

            public TypeAlias(Type type, string alias = null)
            {
                Type = type;
                Alias = string.IsNullOrEmpty(alias) ? type.Name.ToLower() : alias;
            }
        }
    }
}