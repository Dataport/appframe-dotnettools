using Dataport.AppFrameDotNet.DotNetTools.Numeric.Model;
using System;

namespace Dataport.AppFrameDotNet.DotNetTools.Numeric.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für Prozentrechnung mit numerischen Werten zur Verfügung.
    /// </summary>
    public static class Percentage
    {
        /// <summary>
        /// Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.
        /// </summary>
        /// <param name="i">Die Zahl, die zu prüfen ist.</param>
        /// <param name="everything">Die gesamte Menge</param>
        /// <param name="type">Der Typ in dem das Ergebnis zurückgegeben wird.</param>
        /// <returns>Der prozentuale Anteil der Zahl an der Gesamtheit.</returns>
        /// <exception cref="ArgumentException"><paramref name="i"/> oder <paramref name="everything"/> sind kleiner als 0.</exception>
        public static float AsPercentageOf(this short i, short everything, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || everything < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            if (everything == 0)
            {
                throw new ArgumentException("The total amount must be greater 0.");
            }

            return type == PercentageType.Arithmetic ? i * 1.0f / everything : i * 100.0f / everything;
        }

        /// <summary>
        /// Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.
        /// </summary>
        /// <param name="i">Die Zahl, die als 100% angenommen wird.</param>
        /// <param name="percentage">Der prozentuale Anteil</param>
        /// <param name="type">Gibt an, wie <paramref name="percentage"/> zu interpretieren ist.</param>
        /// <returns>Der prozentuale Anteil</returns>
        public static float GetPercentageAmount(this short i, float percentage, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || percentage < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            return type == PercentageType.Arithmetic ? i * 1.0f * percentage : i * 1.0f * (percentage / 100);
        }

        /// <summary>
        /// Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.
        /// </summary>
        /// <param name="i">Die Zahl, die zu prüfen ist.</param>
        /// <param name="everything">Die gesamte Menge</param>
        /// <param name="type">Der Typ in dem das Ergebnis zurückgegeben wird.</param>
        /// <returns>Der prozentuale Anteil der Zahl an der Gesamtheit.</returns>
        /// <exception cref="ArgumentException"><paramref name="i"/> oder <paramref name="everything"/> sind kleiner als 0.</exception>
        public static float AsPercentageOf(this int i, int everything, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || everything < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            if (everything == 0)
            {
                throw new ArgumentException("The total amount must be greater 0.");
            }

            return type == PercentageType.Arithmetic ? i * 1.0f / everything : i * 100f / everything;
        }

        /// <summary>
        /// Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.
        /// </summary>
        /// <param name="i">Die Zahl, die als 100% angenommen wird.</param>
        /// <param name="percentage">Der prozentuale Anteil</param>
        /// <param name="type">Gibt an, wie <paramref name="percentage"/> zu interpretieren ist.</param>
        /// <returns>Der prozentuale Anteil</returns>
        public static float GetPercentageAmount(this int i, float percentage, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || percentage < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            return type == PercentageType.Arithmetic ? i * percentage : i * (percentage / 100);
        }

        /// <summary>
        /// Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.
        /// </summary>
        /// <param name="i">Die Zahl, die zu prüfen ist.</param>
        /// <param name="everything">Die gesamte Menge</param>
        /// <param name="type">Der Typ in dem das Ergebnis zurückgegeben wird.</param>
        /// <returns>Der prozentuale Anteil der Zahl an der Gesamtheit.</returns>
        /// <exception cref="ArgumentException"><paramref name="i"/> oder <paramref name="everything"/> sind kleiner als 0.</exception>
        public static float AsPercentageOf(this long i, long everything, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || everything < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            if (everything == 0)
            {
                throw new ArgumentException("The total amount must be greater 0.");
            }

            return type == PercentageType.Arithmetic ? i * 1.0f / everything : i * 100f / everything;
        }

        /// <summary>
        /// Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.
        /// </summary>
        /// <param name="i">Die Zahl, die als 100% angenommen wird.</param>
        /// <param name="percentage">Der prozentuale Anteil</param>
        /// <param name="type">Gibt an, wie <paramref name="percentage"/> zu interpretieren ist.</param>
        /// <returns>Der prozentuale Anteil</returns>
        public static float GetPercentageAmount(this long i, float percentage, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || percentage < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            return type == PercentageType.Arithmetic ? i * percentage : i * (percentage / 100);
        }

        /// <summary>
        /// Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.
        /// </summary>
        /// <param name="i">Die Zahl, die zu prüfen ist.</param>
        /// <param name="everything">Die gesamte Menge</param>
        /// <param name="type">Der Typ in dem das Ergebnis zurückgegeben wird.</param>
        /// <returns>Der prozentuale Anteil der Zahl an der Gesamtheit.</returns>
        /// <exception cref="ArgumentException"><paramref name="i"/> oder <paramref name="everything"/> sind kleiner als 0.</exception>
        public static float AsPercentageOf(this float i, float everything, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || everything < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            if (everything.Equals(0f))
            {
                throw new ArgumentException("The total amount must be greater 0.");
            }

            return type == PercentageType.Arithmetic ? i / everything : i * 100 / everything;
        }

        /// <summary>
        /// Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.
        /// </summary>
        /// <param name="i">Die Zahl, die als 100% angenommen wird.</param>
        /// <param name="percentage">Der prozentuale Anteil</param>
        /// <param name="type">Gibt an, wie <paramref name="percentage"/> zu interpretieren ist.</param>
        /// <returns>Der prozentuale Anteil</returns>
        public static float GetPercentageAmount(this float i, float percentage, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || percentage < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            return type == PercentageType.Arithmetic ? i * percentage : i * (percentage / 100);
        }

        /// <summary>
        /// Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.
        /// </summary>
        /// <param name="i">Die Zahl, die zu prüfen ist.</param>
        /// <param name="everything">Die gesamte Menge</param>
        /// <param name="type">Der Typ in dem das Ergebnis zurückgegeben wird.</param>
        /// <returns>Der prozentuale Anteil der Zahl an der Gesamtheit.</returns>
        /// <exception cref="ArgumentException"><paramref name="i"/> oder <paramref name="everything"/> sind kleiner als 0.</exception>
        public static double AsPercentageOf(this double i, double everything, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || everything < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            if (everything.Equals(0d))
            {
                throw new ArgumentException("The total amount must be greater 0.");
            }

            return type == PercentageType.Arithmetic ? i / everything : i * 100 / everything;
        }

        /// <summary>
        /// Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.
        /// </summary>
        /// <param name="i">Die Zahl, die als 100% angenommen wird.</param>
        /// <param name="percentage">Der prozentuale Anteil</param>
        /// <param name="type">Gibt an, wie <paramref name="percentage"/> zu interpretieren ist.</param>
        /// <returns>Der prozentuale Anteil</returns>
        public static double GetPercentageAmount(this double i, double percentage, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || percentage < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            return type == PercentageType.Arithmetic ? i * percentage : i * (percentage / 100);
        }

        /// <summary>
        /// Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.
        /// </summary>
        /// <param name="i">Die Zahl, die zu prüfen ist.</param>
        /// <param name="everything">Die gesamte Menge</param>
        /// <param name="type">Der Typ in dem das Ergebnis zurückgegeben wird.</param>
        /// <returns>Der prozentuale Anteil der Zahl an der Gesamtheit.</returns>
        /// <exception cref="ArgumentException"><paramref name="i"/> oder <paramref name="everything"/> sind kleiner als 0.</exception>
        public static decimal AsPercentageOf(this decimal i, decimal everything, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || everything < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            if (everything == 0)
            {
                throw new ArgumentException("The total amount must be greater 0.");
            }

            return type == PercentageType.Arithmetic ? i / everything : i * 100 / everything;
        }

        /// <summary>
        /// Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.
        /// </summary>
        /// <param name="i">Die Zahl, die als 100% angenommen wird.</param>
        /// <param name="percentage">Der prozentuale Anteil</param>
        /// <param name="type">Gibt an, wie <paramref name="percentage"/> zu interpretieren ist.</param>
        /// <returns>Der prozentuale Anteil</returns>
        public static decimal GetPercentageAmount(this decimal i, decimal percentage, PercentageType type = PercentageType.Arithmetic)
        {
            if (i < 0 || percentage < 0)
            {
                throw new ArgumentException("Calculation of percentage is not supported for negative values");
            }

            return type == PercentageType.Arithmetic ? i * percentage : i * (percentage / 100);
        }
    }
}