using Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection.Extensions
{
    public class TypeExtensionsTests
    {
        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(Type))]
        [InlineData(typeof(int?))]
        [InlineData(typeof(List<string>))]
        [InlineData(typeof(IEnumerable<string>))]
        [InlineData(typeof(ISerializable))]
        public void IsNullable_NullableType_ShouldBeTrue(Type type)
        {
            // act
            var result = type.IsNullable();

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(short))]
        [InlineData(typeof(long))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(TestEnum))]
        public void IsNullable_NotNullableType_ShouldBeFalse(Type type)
        {
            // act
            var result = type.IsNullable();

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(typeof(string), typeof(IEnumerable<char>), true)]
        [InlineData(typeof(ListChild), typeof(List<string>), false)]
        [InlineData(typeof(int), typeof(IEnumerable<char>), false)]
        public void Implements_Cases_ReturnsExpectedResult(Type t, Type implementation, bool expectedResult)
        {
            // act
            var result = t.Implements(implementation);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(ListChild), typeof(List<string>), true)]
        [InlineData(typeof(string), typeof(IEnumerable<char>), false)]
        [InlineData(typeof(int), typeof(IEnumerable<char>), false)]
        public void Extends_Cases_ReturnsExpectedResult(Type t, Type extension, bool expectedResult)
        {
            // act
            var result = t.Extends(extension);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(List<string>), true)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(int), false)]
        public void IsGenericCollection_Cases_ReturnsExpectedResult(Type t, bool expectedResult)
        {
            // act
            var result = t.IsGenericCollection();

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(List<string>), true)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(int), false)]
        public void IsGenericEnumerable_Cases_ReturnsExpectedResult(Type t, bool expectedResult)
        {
            // act
            var result = t.IsGenericEnumerable();

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(string), typeof(IEnumerable<>), true)]
        [InlineData(typeof(string), typeof(IEnumerable<char>), true)]
        [InlineData(typeof(ListChild), typeof(List<>), true)]
        [InlineData(typeof(ListChild), typeof(List<string>), true)]
        [InlineData(typeof(int), typeof(IEnumerable<>), false)]
        [InlineData(typeof(int), typeof(IEnumerable<char>), false)]
        [InlineData(typeof(ListChild), typeof(List<int>), false)]
        public void IsGenericTypeOf_Cases_ReturnsExpectedResult(Type t, Type ofType, bool expectedResult)
        {
            // act
            var result = t.IsGenericTypeOf(ofType);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<char>), typeof(string), true)]
        [InlineData(typeof(IEnumerable<int>), typeof(string), false)]
        [InlineData(typeof(string), typeof(int), false)]
        public void IsGenericTypeFor_Cases_ReturnsExpectedResult(Type t, Type forType, bool expectedResult)
        {
            // act
            var result = t.IsGenericTypeFor(forType);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(string), typeof(string), true)]
        [InlineData(typeof(string), typeof(int), false)]
        [InlineData(typeof(IEnumerable<string>), typeof(IEnumerable<string>), true)]
        [InlineData(typeof(IEnumerable<string>), typeof(IEnumerable<int>), false)]
        [InlineData(typeof(List<string>), typeof(IEnumerable<string>), true)]
        [InlineData(typeof(List<string>), typeof(IEnumerable<int>), false)]
        [InlineData(typeof(ListChild), typeof(List<string>), true)]
        [InlineData(typeof(ListChild), typeof(List<int>), false)]
        [InlineData(typeof(ListChild), typeof(IEnumerable<string>), true)]
        [InlineData(typeof(ListChild), typeof(IEnumerable<int>), false)]
        [InlineData(typeof(ListChild), typeof(INestedInterface), true)]
        [InlineData(typeof(object), typeof(object), true)]
        [InlineData(typeof(object), typeof(string), false)]
        [InlineData(typeof(List<string>), typeof(ListChild), false)]
        [InlineData(typeof(IEnumerable<string>), typeof(List<string>), false)]
        public void IsOrIsInheritFrom_Cases_ReturnsExpectedResult(Type t, Type compare, bool expectedResult)
        {
            // act
            var result = t.IsOrIsInheritFrom(compare);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(nameof(MemberClass.PublicStaticProperty))]
        [InlineData(nameof(MemberClass.PublicProperty))]
        [InlineData(nameof(MemberClass.InternalProperty))]
        [InlineData("PrivateProperty")]
        public void GetPropertyByName_Cases_ReturnsProperty(string propertyName)
        {
            // act
            var result = typeof(MemberClass).GetPropertyByName(propertyName);

            // assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData(nameof(ConstantValueClass.PublicFooProp))]
        [InlineData(nameof(ConstantValueClass.PublicFooField))]
        [InlineData(nameof(ConstantValueClass.PublicFooConst))]
        public void GetConstantValue_Cases_ReturnsValue(string memberName)
        {
            // act
            var result = typeof(ConstantValueClass).GetConstantValue<string>(memberName);

            // assert
            result.Should().Be(memberName + "1");
        }

        [Fact]
        public void GetConstantValue_MemberNotExists_ThrowsArgumentException()
        {
            // arrange
            Action fail = () => typeof(ConstantValueClass).GetConstantValue<string>("Invalid");

            // assert
            fail.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(typeof(byte), "byte")]
        [InlineData(typeof(sbyte), "sbyte")]
        [InlineData(typeof(short), "short")]
        [InlineData(typeof(ushort), "ushort")]
        [InlineData(typeof(int), "int")]
        [InlineData(typeof(uint), "uint")]
        [InlineData(typeof(long), "long")]
        [InlineData(typeof(ulong), "ulong")]
        [InlineData(typeof(float), "float")]
        [InlineData(typeof(double), "double")]
        [InlineData(typeof(decimal), "decimal")]
        [InlineData(typeof(object), "object")]
        [InlineData(typeof(bool), "bool")]
        [InlineData(typeof(char), "char")]
        [InlineData(typeof(string), "string")]
        [InlineData(typeof(void), "void")]
        [InlineData(typeof(byte), "Byte", false, true)]
        [InlineData(typeof(sbyte), "SByte", false, true)]
        [InlineData(typeof(short), "Int16", false, true)]
        [InlineData(typeof(ushort), "UInt16", false, true)]
        [InlineData(typeof(int), "Int32", false, true)]
        [InlineData(typeof(uint), "UInt32", false, true)]
        [InlineData(typeof(long), "Int64", false, true)]
        [InlineData(typeof(ulong), "UInt64", false, true)]
        [InlineData(typeof(float), "Single", false, true)]
        [InlineData(typeof(double), "Double", false, true)]
        [InlineData(typeof(decimal), "Decimal", false, true)]
        [InlineData(typeof(object), "Object", false, true)]
        [InlineData(typeof(bool), "Boolean", false, true)]
        [InlineData(typeof(char), "Char", false, true)]
        [InlineData(typeof(string), "String", false, true)]
        [InlineData(typeof(void), "Void", false, true)]
        [InlineData(typeof(Action<>), "Action<T>")]
        [InlineData(typeof(Action<string, int>), "Action<string, int>")]
        [InlineData(typeof(Action<Action<bool>>), "Action<Action<bool>>")]
        [InlineData(typeof(Action<Action<bool>>), "Action<Action<Boolean>>", true, true)]
        [InlineData(typeof(IDictionary<string, object>), "IDictionary<string, object>")]
        [InlineData(typeof(IDictionary<string, object>), "IDictionary<String, Object>", true, true)]
        [InlineData(typeof(IEnumerable<>), "IEnumerable<out T>", true)]
        [InlineData(typeof(IEnumerable<>), "IEnumerable<T>")]
        // Parent class (TypeExtensionsTests) is always included as prefix in codename
        [InlineData(typeof(MemberClass), "TypeExtensionsTests.MemberClass")]
        [InlineData(typeof(GenericMasterClass<>.GenericNestedClass<>), "TypeExtensionsTests.GenericMasterClass<TMaster>.GenericNestedClass<TNested>")]
        [InlineData(typeof(GenericMasterClass<object>.GenericNestedClass<string>), "TypeExtensionsTests.GenericMasterClass<object>.GenericNestedClass<string>")]
        [InlineData(typeof(GenericMasterClass<object>.GenericNestedClass<string>), "TypeExtensionsTests.GenericMasterClass<Object>.GenericNestedClass<String>", false, true)]
        [InlineData(typeof(GenericConstraintsClass<>), "TypeExtensionsTests.GenericConstraintsClass<{T : class, IDisposable, new()}>", true)]
        [InlineData(typeof(GenericConstraintsClass<>), "TypeExtensionsTests.GenericConstraintsClass<T>")]
        [InlineData(typeof(GenericConstraintClassWithGeneric<>), "TypeExtensionsTests.GenericConstraintClassWithGeneric<{T : IEnumerable<String>}>", true, true)]
        [InlineData(typeof(GenericConstraintClassWithGeneric<>), "TypeExtensionsTests.GenericConstraintClassWithGeneric<{T : IEnumerable<string>}>", true)]
        [InlineData(typeof(GenericConstraintClassWithGeneric<>), "TypeExtensionsTests.GenericConstraintClassWithGeneric<T>")]
        [InlineData(typeof(GenericConstraintClassWithNestedGeneric<>), "TypeExtensionsTests.GenericConstraintClassWithNestedGeneric<{T : IEnumerable<ICollection<String>>}>", true, true)]
        [InlineData(typeof(GenericConstraintClassWithNestedGeneric<>), "TypeExtensionsTests.GenericConstraintClassWithNestedGeneric<{T : IEnumerable<ICollection<string>>}>", true)]
        [InlineData(typeof(GenericConstraintClassWithNestedGeneric<>), "TypeExtensionsTests.GenericConstraintClassWithNestedGeneric<T>")]
        [InlineData(typeof(GenericArrayClass<>), "TypeExtensionsTests.GenericArrayClass<T>")]
        [InlineData(typeof(GenericArrayClass<>), "TypeExtensionsTests.GenericArrayClass<{T : IEnumerable<int?[,]>}>", true)]
        [InlineData(typeof(GenericArrayClass<>), "TypeExtensionsTests.GenericArrayClass<{T : IEnumerable<Nullable<Int32>[,]>}>", true, true)]
        // Nullable value types
        [InlineData(typeof(int?), "int?")]
        [InlineData(typeof(bool?), "bool?")]
        [InlineData(typeof(short?), "Nullable<Int16>", false, true)]
        [InlineData(typeof(char?), "Nullable<Char>", false, true)]
        // Arrays
        [InlineData(typeof(object[]), "object[]")]
        [InlineData(typeof(object[,]), "object[,]")]
        [InlineData(typeof(object[,,]), "object[,,]")]
        [InlineData(typeof(float[]), "float[]")]
        [InlineData(typeof(object[]), "Object[]", false, true)]
        [InlineData(typeof(bool[,]), "Boolean[,]", false, true)]
        [InlineData(typeof(Action<string, int>[]), "Action<string, int>[]")]
        [InlineData(typeof(Action<string[]>), "Action<string[]>")]
        [InlineData(typeof(Action<Action<bool>>[]), "Action<Action<Boolean>>[]", true, true)]
        // Array + Nullable value types
        [InlineData(typeof(int?[]), "int?[]")]
        [InlineData(typeof(double?[,]), "double?[,]")]
        [InlineData(typeof(Action<int?[]>), "Action<int?[]>")]
        [InlineData(typeof(Action<int?[,]>), "Action<int?[,]>")]
        [InlineData(typeof(bool?[]), "Nullable<Boolean>[]", false, true)]
        [InlineData(typeof(short?[,]), "Nullable<Int16>[,]", false, true)]
        [InlineData(typeof(Action<long?[]>), "Action<Nullable<Int64>[]>", false, true)]
        [InlineData(typeof(Action<long?[,]>), "Action<Nullable<Int64>[,]>", false, true)]
        public void GetCodeName_Cases_Complies(Type type, string expectedCodeName, bool includeConstraints = false, bool useBuildInNames = false)
        {
            // act
            var result = type.GetCodeName(includeConstraints, useBuildInNames);

            // assert
            result.Should().Be(expectedCodeName);
        }

        private class MemberClass
        {
            public static string PublicStaticProperty { get; set; }
            public string PublicProperty { get; set; }
            internal string InternalProperty { get; set; }

            [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Wird implizit benutzt")]
            private string PrivateProperty { get; set; }
        }

        private class ListChild : List<string>, IListChild
        {
        }

        private interface IListChild : INestedInterface
        {
        }

        private interface INestedInterface
        {
        }

        private enum TestEnum
        {
        }

        private class ConstantValueClass
        {
            public static string PublicFooProp { get; } = "PublicFooProp1";
            public static string PublicFooField = "PublicFooField1";
            public const string PublicFooConst = "PublicFooConst1";
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class GenericMasterClass<TMaster>
        {
            // ReSharper disable once UnusedMember.Local
            public TMaster Master { get; set; }

            public class GenericNestedClass<TNested>
            {
                // ReSharper disable once UnusedMember.Local
                public GenericMasterClass<TMaster> Owner { get; set; }

                // ReSharper disable once UnusedMember.Local
                public TNested Value { get; set; }
            }
        }

        private class GenericConstraintsClass<T>
            where T : class, IDisposable, new()
        {
            // ReSharper disable once UnusedMember.Local
            public IDisposable Create() => new T();
        }

        // ReSharper disable once UnusedTypeParameter
        private class GenericConstraintClassWithGeneric<T> where T : IEnumerable<string>
        {
        }

        // ReSharper disable once UnusedTypeParameter
        private class GenericConstraintClassWithNestedGeneric<T> where T : IEnumerable<ICollection<string>>
        {
        }

        // ReSharper disable once UnusedTypeParameter
        private class GenericArrayClass<T> where T : IEnumerable<int?[,]>
        {
        }
    }
}