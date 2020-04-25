using System;

namespace CSharpCore.Helpers
{
    public enum DateTimeFrequency
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Quarterly = 4,
        Annually = 5
    }

    public enum DateTimeTens
    {
        Rolling = 0,
        Future = 1
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class DateTimeHelper
    {
        /// <summary>
        /// Get date time range to the base date
        /// </summary>
        /// <param name="basedate"></param>
        /// <param name="dateTimeFrequency"></param>
        /// <param name="formate"></param>
        /// <returns></returns>
        public static string[] GetDateRange(DateTime basedate, DateTimeFrequency dateTimeFrequency, string formate)
        {
            string[] result = new string[2];
            DateTime dateRangeBegin = basedate;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            DateTime dateRangeEnd = DateTime.Today.Add(duration);

            if (string.IsNullOrEmpty(formate))
                formate = "yyyy-MM-ddTHH:mm:ss";

            switch (dateTimeFrequency)
            {
                case DateTimeFrequency.Daily:
                    dateRangeBegin = basedate;
                    dateRangeEnd = dateRangeBegin;
                    break;

                case DateTimeFrequency.Weekly:
                    dateRangeBegin = basedate.AddDays(-(int)basedate.DayOfWeek);
                    dateRangeEnd = basedate.AddDays(6 - (int)basedate.DayOfWeek);
                    break;

                case DateTimeFrequency.Monthly:
                    duration = new TimeSpan(DateTime.DaysInMonth(basedate.Year, basedate.Month) - 1, 0, 0, 0);
                    dateRangeBegin = basedate.AddDays((-1) * basedate.Day + 1);
                    dateRangeEnd = dateRangeBegin.Add(duration);
                    break;

                case DateTimeFrequency.Quarterly:
                    int currentQuater = (basedate.Date.Month - 1) / 3 + 1;
                    int daysInLastMonthOfQuarter = DateTime.DaysInMonth(basedate.Year, 3 * currentQuater);
                    dateRangeBegin = new DateTime(basedate.Year, 3 * currentQuater - 2, 1);
                    dateRangeEnd = new DateTime(basedate.Year, 3 * currentQuater, daysInLastMonthOfQuarter);
                    break;

                case DateTimeFrequency.Annually:
                    dateRangeBegin = new DateTime(basedate.Year, 1, 1);
                    dateRangeEnd = new DateTime(basedate.Year, 12, 31);
                    break;
            }
            result[0] = dateRangeBegin.Date.ToString(formate);
            result[1] = dateRangeEnd.Date.ToString(formate);
            return result;
        }

        /// <summary>
        /// Get date time range by month to the base date
        /// </summary>
        /// <param name="basedate"></param>
        /// <param name="dateTimeTens"></param>
        /// <param name="months"></param>
        /// <param name="formate"></param>
        /// <returns></returns>
        public static string[] GetDateRangeByMonth(DateTime basedate, DateTimeTens dateTimeTens, int months, string formate)
        {
            string[] result = new string[2];
            DateTime dateRangeBegin = basedate;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            DateTime dateRangeEnd = DateTime.Today.Add(duration);

            if (string.IsNullOrEmpty(formate))
                formate = "yyyy-MM-ddTHH:mm:ss";

            switch(dateTimeTens)
            {
                case DateTimeTens.Rolling:
                    dateRangeBegin = basedate.AddMonths(-months);
                    dateRangeEnd = basedate;
                    break;

                case DateTimeTens.Future:
                    dateRangeBegin = basedate;
                    dateRangeEnd = basedate.AddMonths(months);
                    break;
            }

            result[0] = dateRangeBegin.Date.ToString(formate);
            result[1] = dateRangeEnd.Date.ToString(formate);
            return result;
        }
    }
}
