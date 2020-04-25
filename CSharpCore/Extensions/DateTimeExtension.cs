using System;

namespace CSharpCore.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class DateTimeExtension
    {
        /// <summary>
        /// Conver the datetime to Humar readable formate
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ConvertToHumanTime(this DateTime? dateTime)
        {
            var now = DateTime.Now.ToLocalTime();
            var timestamp = dateTime?.ToLocalTime() ?? DateTime.Now;
            var timespan = TimeSpan.FromTicks(Math.Max(now.Subtract(timestamp).Ticks, TimeSpan.FromSeconds(1).Ticks)); // Ensure that the minimum TimeSpan is 1 second
            var timespanText = string.Empty;

            if (timespan.TotalSeconds < 60)
            {
                var time = Math.Floor(Math.Max(timespan.TotalSeconds, 1));
                timespanText = time > 1 ? $"{time} seconds" : $"{time} second";
            }
            else if (timespan.TotalMinutes < 60)
            {
                var time = Math.Floor(Math.Max(timespan.TotalMinutes, 1));
                timespanText = time > 1 ? $"{time} minutes" : $"{time} minute";
            }
            else if (timespan.TotalHours < 24)
            {
                var time = Math.Floor(Math.Max(timespan.TotalHours, 1));
                timespanText = time > 1 ? $"{time} hours" : $"{time} hour";
            }
            else
            {
                var time = Math.Floor(Math.Max(timespan.TotalDays, 1));
                timespanText = time > 1 ? $"{time} days" : $"{time} day";
            }

            return timespanText;
        }
    }
}
