using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dataport.AppFrameDotNet.DotNetTools.Comparison.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Validation;
using Dataport.AppFrameDotNet.DotNetTools.Validation.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection
{
    // Die Klasse arbeitet leider nicht so typsicher wie ich es gern hätte, daher ist diese internal.
    // Sie wird durch von der generischen Klasse gewrappt und damit typsicher public.
    // In den Enum-Extensions können einzelne Funktionen davon typsicher angesprochen werden.
    internal class EnumHelper
    {
        private readonly Type _enumType;
        private readonly Type _underlyingEnumType;

        public EnumHelper(Enum value)
        {
            _enumType = value.GetType();
            _underlyingEnumType = Enum.GetUnderlyingType(_enumType);
        }

        public int Count()
        {
            return Enum.GetNames(_enumType).Length;
        }

        public IEnumerable<object> GetValues()
        {
            return Enum.GetValues(_enumType).Cast<object>();
        }

        public IEnumerable<object> GetValuesWhereAttribute<TAttribute>(Func<TAttribute, bool> condition) where TAttribute : Attribute
        {
            return GetValues()
                .Select(v => new Tuple<object, TAttribute>(v, v == null ? null : _enumType.GetField(v.ToString() ?? string.Empty)?.GetCustomAttribute<TAttribute>()))
                .Where(kvp => kvp.Item2 != null && condition(kvp.Item2))
                .Select(kvp => kvp.Item1);
        }

        public IDictionary<int, string> AsDictionary()
        {
            return AsDictionary<int>();
        }

        public IDictionary<TKey, string> AsDictionary<TKey>()
        {
            Verify.That(typeof(TKey), "Key").IsEqualTo(_underlyingEnumType);

            var result = new Dictionary<TKey, string>();
            foreach (var value in GetValues())
            {
                result.Add((TKey)Convert.ChangeType(value, _underlyingEnumType), value.ToString());
            }

            return result;
        }

        public bool IsSubstituteOf<TOther>() where TOther : Enum
        {
            if (_enumType == typeof(TOther))
            {
                return true;
            }

            if (Enum.GetUnderlyingType(typeof(TOther)) != _underlyingEnumType)
            {
                return false;
            }

            var localValues = AsDictionaryInternal();
            var otherValues = new EnumHelper(default(TOther)).AsDictionaryInternal();

            return localValues.IsEquivalentTo(otherValues);
        }

        public bool FitsInto<TOther>() where TOther : Enum
        {
            if (_enumType == typeof(TOther))
            {
                return true;
            }

            if (Enum.GetUnderlyingType(typeof(TOther)) != _underlyingEnumType)
            {
                return false;
            }

            var localValues = AsDictionaryInternal();
            var otherValues = new EnumHelper(default(TOther)).AsDictionaryInternal();

            return localValues.IsSubsetOf(otherValues);
        }

        public bool FitsByNameInto<TOther>() where TOther : Enum
        {
            if (_enumType == typeof(TOther))
            {
                return true;
            }

            if (Enum.GetUnderlyingType(typeof(TOther)) != _underlyingEnumType)
            {
                return false;
            }

            var localValues = Enum.GetNames(_enumType);
            var otherValues = Enum.GetNames(typeof(TOther));

            return localValues.All(l => otherValues.Contains(l));
        }

        public bool FitsByIndexInto<TOther>() where TOther : Enum
        {
            if (_enumType == typeof(TOther))
            {
                return true;
            }

            if (Enum.GetUnderlyingType(typeof(TOther)) != _underlyingEnumType)
            {
                return false;
            }

            var localIndexes = AsDictionaryInternal().Keys.ToArray();
            var otherIndexes = new EnumHelper(default(TOther)).AsDictionaryInternal().Keys.ToArray();

            return localIndexes.All(l => otherIndexes.Contains(l));
        }

        public TOut ChangeType<TOut>(Enum value, bool strict = false)
            where TOut : struct, Enum
        {
            Verify.That(value.GetType(), "ValueType").IsEqualTo(_enumType);

            if (strict)
            {
                Verify.That(_enumType, "OriginalType").Is(t => FitsInto<TOut>(), $"The Type '{typeof(TOut)}' is no substitution of '{_enumType}'.");
                return (TOut)Enum.Parse(typeof(TOut), value.ToString());
            }

            if (Enum.TryParse(value.ToString(), out TOut result))
            {
                return result;
            }

            var localValues = AsDictionaryInternal();
            var otherValues = new EnumHelper(default(TOut)).AsDictionaryInternal();

            var index = localValues.Single(v => v.Value == value.ToString()).Key;
            if (otherValues.ContainsKey(index))
            {
                return (TOut)Enum.Parse(typeof(TOut), otherValues[index]);
            }

            throw new ArgumentException($"'{value}' is no member of '{typeof(TOut)}'.");
        }

        private IDictionary<IComparable, string> AsDictionaryInternal()
        {
            if (_underlyingEnumType == typeof(sbyte))
            {
                return AsDictionary<sbyte>().DowngradeKeyType<sbyte, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(byte))
            {
                return AsDictionary<byte>().DowngradeKeyType<byte, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(short))
            {
                return AsDictionary<short>().DowngradeKeyType<short, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(ushort))
            {
                return AsDictionary<ushort>().DowngradeKeyType<ushort, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(int))
            {
                return AsDictionary<int>().DowngradeKeyType<int, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(uint))
            {
                return AsDictionary<uint>().DowngradeKeyType<uint, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(long))
            {
                return AsDictionary<long>().DowngradeKeyType<long, IComparable, string>();
            }

            if (_underlyingEnumType == typeof(ulong))
            {
                return AsDictionary<ulong>().DowngradeKeyType<ulong, IComparable, string>();
            }

            throw new NotSupportedException("Unexpected underlying type detected. This should not happen.");
        }
    }
}