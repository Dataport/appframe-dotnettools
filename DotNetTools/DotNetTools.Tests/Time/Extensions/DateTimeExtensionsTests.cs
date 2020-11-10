using System;
using System.Collections;
using System.Collections.Generic;
using Dataport.AppFrameDotNet.DotNetTools.Time.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Time.Model;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Time.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [ClassData(typeof(FirstDayOfMonthCases))]
        public void FirstDayOfMonth_Cases_ReturnsExpectedResult(DateTime input, DateTime expected)
        {
            // act
            var result = input.FirstDayOfMonth();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(LastDayOfMonthCases))]
        public void LastDayOfMonth_Cases_ReturnsExpectedResult(DateTime input, DateTime expected)
        {
            // act
            var result = input.LastDayOfMonth();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(FirstDayOfNextMonthCases))]
        public void FirstDayOfNextMonth_Cases_ReturnsExpectedResult(DateTime input, DateTime expected)
        {
            // act
            var result = input.FirstDayOfNextMonth();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(TruncateCases))]
        public void Truncate_Cases_ReturnsExpectedResult(DateTime input, DateTimePart mode, DateTime expected)
        {
            // act
            var result = input.Truncate(mode);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(BeginOfDayCases))]
        public void BeginOfDay_Cases_ReturnsExpectedResult(DateTime input, DateTime expected)
        {
            // act
            var result = input.BeginOfDay();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(EndOfDayCases))]
        public void EndOfDay_Cases_ReturnsExpectedResult(DateTime input, DateTime expected)
        {
            // act
            var result = input.EndOfDay();

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(SetDayValidCases))]
        public void SetDay_ValidCases_ReturnsExpectedResult(DateTime input, int day, DateTime expected)
        {
            // act
            var result = input.SetDay(day);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(SetDayInvalidCases))]
        public void SetDay_InvalidCases_ThrowsException(DateTime input, int day)
        {
            // arrange
            Action fail = () => input.SetDay(day);

            // act + assert
            fail.Should().Throw<ArgumentOutOfRangeException>();
        }

        private class FirstDayOfMonthCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12), new DateTime(2020, 1, 1) };
                yield return new object[] { new DateTime(2020, 1, 12, 11, 11, 11), new DateTime(2020, 1, 1) };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 1) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class LastDayOfMonthCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12), new DateTime(2020, 1, 31) };
                yield return new object[] { new DateTime(2020, 1, 12, 11, 11, 11), new DateTime(2020, 1, 31) };
                yield return new object[] { new DateTime(2020, 1, 31), new DateTime(2020, 1, 31) };
                yield return new object[] { new DateTime(2020, 4, 30), new DateTime(2020, 4, 30) };
                yield return new object[] { new DateTime(2019, 12, 30), new DateTime(2019, 12, 31) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class FirstDayOfNextMonthCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12), new DateTime(2020, 2, 1) };
                yield return new object[] { new DateTime(2020, 1, 12, 11, 11, 11), new DateTime(2020, 2, 1) };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 2, 1) };
                yield return new object[] { new DateTime(2019, 12, 20), new DateTime(2020, 1, 1) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class TruncateCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Milliseconds, new DateTime(2020, 2, 12, 12, 34, 56) };
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Seconds, new DateTime(2020, 2, 12, 12, 34, 0) };
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Minutes, new DateTime(2020, 2, 12, 12, 0, 0) };
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Hours, new DateTime(2020, 2, 12) };
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Days, new DateTime(2020, 2, 1) };
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Months, new DateTime(2020, 1, 1) };
                yield return new object[] { new DateTime(2020, 2, 12, 12, 34, 56, 78), DateTimePart.Year, DateTime.MinValue };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class BeginOfDayCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12, 0, 0, 0), new DateTime(2020, 1, 12, 0, 0, 0, 0) };
                yield return new object[] { new DateTime(2020, 1, 12, 11, 11, 11, 11), new DateTime(2020, 1, 12, 0, 0, 0, 0) };
                yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2020, 1, 1, 0, 0, 0, 0) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class EndOfDayCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12, 0, 0, 0, 0), new DateTime(2020, 1, 12, 23, 59, 59, 999) };
                yield return new object[] { new DateTime(2020, 1, 12, 11, 11, 11, 11), new DateTime(2020, 1, 12, 23, 59, 59, 999) };
                yield return new object[] { new DateTime(2020, 1, 1, 23, 59, 59, 999), new DateTime(2020, 1, 1, 23, 59, 59, 999) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class SetDayValidCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12), 1, new DateTime(2020, 1, 1) };
                yield return new object[] { new DateTime(2020, 1, 12), 15, new DateTime(2020, 1, 15) };
                yield return new object[] { new DateTime(2020, 1, 12), 31, new DateTime(2020, 1, 31) };
                yield return new object[] { new DateTime(2020, 1, 12, 11, 11, 11, 11), 15, new DateTime(2020, 1, 15, 11, 11, 11, 11) };
                yield return new object[] { new DateTime(2020, 2, 1), 29, new DateTime(2020, 2, 29) }; // das geht, weil Schaltjahr
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class SetDayInvalidCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new DateTime(2020, 1, 12), -5 };
                yield return new object[] { new DateTime(2020, 1, 12), 0 };
                yield return new object[] { new DateTime(2020, 1, 12), 32 };
                yield return new object[] { new DateTime(2021, 2, 1), 29 };
                yield return new object[] { new DateTime(2021, 4, 1), 31 };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}