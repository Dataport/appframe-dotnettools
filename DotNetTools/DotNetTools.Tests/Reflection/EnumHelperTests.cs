using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dataport.AppFrameDotNet.DotNetTools.Reflection;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Reflection
{
    [SuppressMessage("ReSharper", "UnusedType.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class EnumHelperTests
    {
        [Fact]
        public void Count_Default_ReturnsNumberOfEnumMember()
        {
            // act
            var result = EnumHelper<TestEnum>.Count();

            // assert
            result.Should().Be(3);
        }

        [Fact]
        public void GetValues_Default_ReturnsAllValues()
        {
            // act
            var result = EnumHelper<TestEnum>.GetValues().ToArray();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(TestEnum.A);
            result.Should().Contain(TestEnum.B);
            result.Should().Contain(TestEnum.C);
        }

        [Fact]
        public void GetValuesWhereAttribute_AttributeNotExists_ReturnsNoElement()
        {
            // act
            var result = EnumHelper<TestEnum>.GetValuesWhereAttribute<TestAttribute>(a => true).ToArray();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetValuesWhereAttribute_AttributeExistOnlyOnSingleElements_ReturnsSingleElement()
        {
            // act
            var result = EnumHelper<TestEnum>.GetValuesWhereAttribute<DefaultValueAttribute>(a => true).ToArray();

            // assert
            result.Should().HaveCount(1);
            result.Single().Should().Be(TestEnum.B);
        }

        [Fact]
        public void GetValuesWhereAttribute_NoMatch_ReturnsNoElements()

        {
            // act
            var result = EnumHelper<TestEnum>.GetValuesWhereAttribute<DescriptionAttribute>(a => false).ToArray();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetValuesWhereAttribute_SingleMatch_ReturnsExpectedElement()
        {
            // act
            var result = EnumHelper<TestEnum>.GetValuesWhereAttribute<DescriptionAttribute>(a => a.Description == "Zeh").ToArray();

            // assert
            result.Should().HaveCount(1);
            result.Single().Should().Be(TestEnum.C);
        }

        [Fact]
        public void GetValuesWhereAttribute_MultipleMatch_ReturnsExpectedElement()
        {
            // act
            var result = EnumHelper<TestEnum>.GetValuesWhereAttribute<DescriptionAttribute>(a => a.Description != "Zeh").ToArray();

            // assert
            result.Should().HaveCount(2);
            result.Should().Contain(TestEnum.A);
            result.Should().Contain(TestEnum.B);
        }

        [Fact]
        public void AsDictionary_DefaultStructure_ReturnsExpectedStructure()
        {
            // act
            var result = EnumHelper<TestEnum>.AsDictionary();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(0, "A");
            result.Should().Contain(1, "B");
            result.Should().Contain(2, "C");
        }

        [Fact]
        public void AsDictionary_OtherKeyStructureWithoutType_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<LongTestEnum>.AsDictionary();

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AsDictionary_OtherKeyStructure_ReturnsExpectedStructure()
        {
            // act
            var result = EnumHelper<LongTestEnum>.AsDictionary<long>();

            // assert
            result.Should().HaveCount(3);
            result.Should().Contain(0, "A");
            result.Should().Contain(1, "B");
            result.Should().Contain(2, "C");
        }

        [Fact]
        public void AsDictionary_OtherKeyStructureWithFalseType_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.AsDictionary<long>();

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void IsSubstituteOf_SameEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<TestEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSubstituteOf_SubstituteEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<SubstituteEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSubstituteOf_SubstituteEnumWithOtherKey_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<LongTestEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubstituteOf_BiggerEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<BiggerEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubstituteOf_SameNameEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<SameNameEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubstituteOf_SameIndexEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<SameIndexEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsSubstituteOf_LesserEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.IsSubstituteOf<LesserEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsInto_SameEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<TestEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsInto_SubstituteEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<SubstituteEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsInto_SubstituteEnumWithOtherKey_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<LongTestEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsInto_BiggerEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<BiggerEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsInto_SameNameEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<SameNameEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsInto_SameIndexEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<SameIndexEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsInto_LesserEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsInto<LesserEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsByNameInto_SameEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<TestEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByNameInto_SubstituteEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<SubstituteEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByNameInto_SubstituteEnumWithOtherKey_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<LongTestEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsByNameInto_BiggerEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<BiggerEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByNameInto_SameNameEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<SameNameEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByNameInto_SameIndexEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<SameIndexEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsByNameInto_LesserEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByNameInto<LesserEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsByIndexInto_SameEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<TestEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByIndexInto_SubstituteEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<SubstituteEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByIndexInto_SubstituteEnumWithOtherKey_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<LongTestEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsByIndexInto_BiggerEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<BiggerEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByIndexInto_SameNameEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<SameNameEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void FitsByIndexInto_SameIndexEnum_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<SameIndexEnum>();

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FitsByIndexInto_LesserEnum_ReturnsFalse()
        {
            // act
            var result = EnumHelper<TestEnum>.FitsByIndexInto<LesserEnum>();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ChangeType_SubstituteEnum_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<SubstituteEnum>(TestEnum.B);

            // assert
            result.Should().Be(SubstituteEnum.B);
        }

        [Fact]
        public void ChangeType_SubstituteEnumWithOtherKey_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<LongTestEnum>(TestEnum.B);

            // assert
            result.Should().Be(LongTestEnum.B);
        }

        [Fact]
        public void ChangeType_BiggerEnum_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<BiggerEnum>(TestEnum.B);

            // assert
            result.Should().Be(BiggerEnum.B);
        }

        [Fact]
        public void ChangeType_SameNameEnum_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<SameNameEnum>(TestEnum.B);

            // assert
            result.Should().Be(SameNameEnum.B);
        }

        [Fact]
        public void ChangeType_SameIndexEnum_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<SameIndexEnum>(TestEnum.B);

            // assert
            result.Should().Be(SameIndexEnum.Y);
        }

        [Fact]
        public void ChangeType_LesserEnumWithHit_ReturnsTrue()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<LesserEnum>(TestEnum.B);

            // assert
            result.Should().Be(LesserEnum.B);
        }

        [Fact]
        public void ChangeType_LesserEnumWithoutHit_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.ChangeType<LesserEnum>(TestEnum.C);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ChangeType_SubstituteEnumStrict_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<SubstituteEnum>(TestEnum.B, true);

            // assert
            result.Should().Be(SubstituteEnum.B);
        }

        [Fact]
        public void ChangeType_SubstituteEnumWithOtherKeyStrict_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.ChangeType<LongTestEnum>(TestEnum.B, true);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ChangeType_BiggerEnumStrict_Works()
        {
            // act
            var result = EnumHelper<TestEnum>.ChangeType<BiggerEnum>(TestEnum.B, true);

            // assert
            result.Should().Be(BiggerEnum.B);
        }

        [Fact]
        public void ChangeType_SameNameEnumStrict_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.ChangeType<SameNameEnum>(TestEnum.B, true);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ChangeType_SameIndexEnumStrict_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.ChangeType<SameIndexEnum>(TestEnum.B, true);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ChangeType_LesserEnumWithHitStrict_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.ChangeType<LesserEnum>(TestEnum.B, true);

            // act + assert
            fail.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ChangeType_LesserEnumWithoutHitStrict_ThrowsException()
        {
            // arrange
            Action fail = () => EnumHelper<TestEnum>.ChangeType<LesserEnum>(TestEnum.C, true);

            // act
            fail.Should().Throw<ArgumentException>();
        }

        private enum TestEnum
        {
            [Description("Ah")]
            A,

            [Description("Beh")]
            [DefaultValue("bb")]
            B,

            [Description("Zeh")]
            C
        }

        private enum LongTestEnum : long
        {
            A,
            B,
            C
        }

        private enum SubstituteEnum
        {
            A,
            B,
            C
        }

        private enum BiggerEnum
        {
            A,
            B,
            C,
            D,
            E,
            F
        }

        private enum SameNameEnum
        {
            A = 15,
            B = 16,
            C = 17
        }

        private enum SameIndexEnum
        {
            X,
            Y,
            Z
        }

        private enum LesserEnum
        {
            A,
            B
        }

        private class TestAttribute : Attribute
        {
        }
    }
}