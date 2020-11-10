using System;
using System.Collections.Generic;
using Dataport.AppFrameDotNet.DotNetTools.Text.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.Numeric.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für die Darstellung numerischer Werte zur Verfügung.
    /// </summary>
    public static class Formatting
    {
        /// <summary>
        /// Fügt einer Zahl führende Nullen hinzu.
        /// </summary>
        /// <param name="i">Die Zahl, die formatiert werden soll.</param>
        /// <param name="lengthOfHighestNumber">Die Länge der höchsten Zahl, als Referenz für die Formatierung.</param>
        /// <returns>Die formatierte Zahl</returns>
        /// <exception cref="NotSupportedException">Die Zahl ist kleiner als 0.</exception>
        /// <exception cref="ArgumentException">Die <paramref name="lengthOfHighestNumber"/> ist kleiner als 1 oder kleiner als die Länge der zu formatierenden Zahl.</exception>
        public static string WithLeadingZeros(this int i, int lengthOfHighestNumber)
        {
            if (lengthOfHighestNumber < 1)
            {
                throw new ArgumentException("The length of the reference must be greater 0");
            }

            int numberLength = i.CountDigits();
            if (numberLength > lengthOfHighestNumber)
            {
                throw new ArgumentException("Number is higher than the reference given");
            }
            return i.ToString("D" + lengthOfHighestNumber);
        }

        /// <summary>
        /// Transformiert die übergebene Zahl in einen zusammenhängenden deutschen String.
        /// </summary>
        /// <example>
        /// 10572.AsWords(); //"Zehntausendfünfhundertzweiundsiebzig"
        /// </example>
        /// <param name="i">Die Zahl die überführt werden soll.</param>
        /// <returns>Die String-Representation der Zahl.</returns>
        public static string AsWords(this short i)
        {
            return new DefaultGermanNumberFormatter(i).AsWords();
        }

        /// <summary>
        /// Transformiert die übergebene Zahl in einen zusammenhängenden deutschen String.
        /// </summary>
        /// <example>
        /// 222318.AsWords(); //"Zweihundertzweiundzwanzigtausenddreihundertachtzehn"
        /// </example>
        /// <param name="i">Die Zahl die überführt werden soll.</param>
        /// <returns>Die String-Representation der Zahl.</returns>
        public static string AsWords(this int i)
        {
            return new DefaultGermanNumberFormatter(i).AsWords();
        }

        /// <summary>
        /// Transformiert die übergebene Zahl in einen zusammenhängenden deutschen String.
        /// </summary>
        /// <example>
        /// long.MaxValue.AsWords(); //"Neuntrillionenzweihundertdreiundzwanzigbilliardendreihundertzweiundsiebzigbillionensechsunddreißigmilliardenachthundertvierundfünfzigmillionensiebenhundertfünfundsiebzigtausendachthundertsieben"
        /// </example>
        /// <param name="i">Die Zahl die überführt werden soll.</param>
        /// <returns>Die String-Representation der Zahl.</returns>
        public static string AsWords(this long i)
        {
            return new DefaultGermanNumberFormatter(i).AsWords();
        }

        private class DefaultGermanNumberFormatter
        {
            private static readonly Dictionary<string, string> NumericWords = new Dictionary<string, string>
            {
                { "1", "eins" },
                { "2", "zwei" },
                { "3", "drei" },
                { "4", "vier" },
                { "5", "fünf" },
                { "6", "sechs" },
                { "7", "sieben" },
                { "8", "acht" },
                { "9", "neun" },
                { "10", "zehn" },
                { "11", "elf" },
                { "12", "zwölf" },
                { "13", "dreizehn" },
                { "14", "vierzehn" },
                { "15", "fünfzehn" },
                { "16", "sechzehn" },
                { "17", "siebzehn" },
                { "18", "achtzehn" },
                { "19", "neunzehn" },
                { "20", "zwanzig" },
                { "30", "dreißig" },
                { "40", "vierzig" },
                { "50", "fünfzig" },
                { "60", "sechzig" },
                { "70", "siebzig" },
                { "80", "achzig" },
                { "90", "neunzig" },
                { "100", "hundert" },
                { "1000", "tausend" },
                { "1000000", "million" },
                { "1000000000", "milliarde" },
                { "1000000000000", "billion" },
                { "1000000000000000", "billiarde" },
                { "1000000000000000000", "trillion" },
            };

            private static readonly Dictionary<string, string> NumericWordsPlural = new Dictionary<string, string>
            {
                { "1000", "tausend" },
                { "1000000", "millionen" },
                { "1000000000", "milliarden" },
                { "1000000000000", "billionen" },
                { "1000000000000000", "billiarden" },
                { "1000000000000000000", "trillionen" },
            };

            private readonly string _number;
            private readonly List<string> _subStrings = new List<string>();

            public DefaultGermanNumberFormatter(short number) : this(number.ToString())
            {
            }

            public DefaultGermanNumberFormatter(int number) : this(number.ToString())
            {
            }

            public DefaultGermanNumberFormatter(long number) : this(number.ToString())
            {
            }

            private DefaultGermanNumberFormatter(string str)
            {
                if (str[0] == '-')
                {
                    _subStrings.Add("Minus ");
                    str = str.Substring(1);
                }

                _number = str;
            }

            public string AsWords()
            {
                AsWordsInternal(_number);
                return Format();
            }

            private void AsWordsInternal(string subString)
            {
                subString = subString.TrimStart('0');
                var length = subString.Length;

                switch (length)
                {
                    case 0:
                        break;
                    case 1:
                        HandleOneDigit(subString);
                        break;
                    case 2:
                        HandleTwoDigits(subString);
                        break;
                    case 3:
                        HandleThreeDigits(subString);
                        break;
                    default:
                        HandleMoreThanTreeDigits(subString);
                        break;
                }
            }

            private void HandleOneDigit(string str)
            {
                _subStrings.Add(NumericWords[str]);
            }

            private void HandleTwoDigits(string str)
            {
                if (str[0] == '1' || str[1] == '0')
                {
                    _subStrings.Add(NumericWords[str]);
                }
                else
                {
                    _subStrings.Add($"{NumericWords[str[1].ToString()]}und");
                    _subStrings.Add(NumericWords[str[0] + "0"]);
                }
            }

            private void HandleThreeDigits(string str)
            {
                _subStrings.Add(str[0] == '1' ? "ein" : NumericWords[str[0].ToString()]);
                _subStrings.Add(NumericWords["100"]);
                AsWordsInternal(str.Substring(1));
            }

            private void HandleMoreThanTreeDigits(string str)
            {
                var length = str.Length;
                var offset = length % 3 == 0 ? 3 : length % 3;

                if (offset == 1 && str[0] == '1')
                {
                    _subStrings.Add($"eine{NumericWords["1" + new string('0', length - offset)]}");
                }
                else
                {
                    AsWordsInternal(str.Substring(0, offset));
                    _subStrings.Add(NumericWordsPlural["1" + new string('0', length - offset)]);
                }
                AsWordsInternal(str.Substring(offset));
            }

            private string Format()
            {
                if (_subStrings.Count == 0)
                {
                    return "Null";
                }

                var result = string.Join("", _subStrings);
                return result.FirstLetterUpperCase();
            }
        }
    }
}