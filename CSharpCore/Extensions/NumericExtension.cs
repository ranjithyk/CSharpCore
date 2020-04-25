using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CSharpCore.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class NumericExtension
    {
        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        public static string ToShortSting(this decimal number, bool withDecimals)
        {
            return ToShortStingInternal(number,CultureInfo.CurrentCulture, withDecimals);
        }

        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        public static string ToShortSting(this decimal number, CultureInfo cultureInfo, bool withDecimals)
        {
            return ToShortStingInternal(number, cultureInfo, withDecimals);
        }

        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        public static string ToShortSting(this double number, bool withDecimals)
        {
            return ToShortStingInternal(Convert.ToDecimal(number), CultureInfo.CurrentCulture, withDecimals);
        }

        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        public static string ToShortSting(this double number, CultureInfo cultureInfo, bool withDecimals)
        {
            return ToShortStingInternal(Convert.ToDecimal(number), cultureInfo, withDecimals);
        }

        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        public static string ToShortSting(this int number, bool withDecimals)
        {
            return ToShortStingInternal(number, CultureInfo.CurrentCulture, withDecimals);
        }

        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        public static string ToShortSting(this int number, CultureInfo cultureInfo, bool withDecimals)
        {
            return ToShortStingInternal(number, cultureInfo, withDecimals);
        }

        /// <summary>
        /// Converts to K, M, B
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="withDecimals"></param>
        /// <returns></returns>
        private static string ToShortStingInternal(decimal number, CultureInfo cultureInfo, bool withDecimals)
        {
            if (number > 999999999 || number < -999999999)
            {
                return number.ToString("0,,,.###B", cultureInfo);
            }
            else if (number > 999999 || number < -999999)
            {
                return number.ToString("0,,.##M", cultureInfo);
            }
            else if (number > 999 || number < -999)
            {
                return number.ToString("0,.#K", cultureInfo);
            }
            else if (withDecimals)
            {
                return string.Format(cultureInfo, "{0:n}", number);
            }
            else
            { 
                return string.Format(cultureInfo, "{0:n0}", number);
            }
        }
    }
}
