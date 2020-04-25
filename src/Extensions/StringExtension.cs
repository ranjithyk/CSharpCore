using System;

namespace CSharpCore.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class StringExtension
    {
        /// <summary>
        /// checks whether given string is part of the source string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(toCheck))
                return true;

            return source?.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Truncate the string to the given length with '...'
        /// </summary>
        /// <param name="input"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string TruncateAt(this string input, int count)
        {
            if (input == null || input.Length < count)
            {
                return input;
            }
            else
            {
                return string.Format("{0}...", input.Substring(0, count));
            }
        }

        /// <summary>
        /// Converts given string to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Convert string to enum.
        /// Set default value is it invalid string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue, bool ignoreCase = true)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, ignoreCase);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert to Julian datetime
        /// </summary>
        /// <param name="julianDateTime"></param>
        /// <returns></returns>
        public static DateTime ToJulianDateTime(this string julianDateTime)
        {
            return new DateTime(Convert.ToInt32(julianDateTime) / 1000, 1, 1).AddMonths(Convert.ToInt32(julianDateTime) % 1000 - 1);
        }

        /// <summary>
        /// Convert string to Guid
        /// If string is invalid Guid formate, Returns new guid
        /// </summary>
        /// <param name="guidString"></param>
        /// <returns></returns>
        public static Guid ToGuid(string guidString)
        {
            Guid guidOutput;
            Guid.TryParse(guidString, out guidOutput);

            return guidOutput == Guid.Empty ? Guid.NewGuid() : guidOutput;
        }
    }
}
