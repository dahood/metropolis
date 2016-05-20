using System;

namespace Metropolis.Api.Utilities
{
    public static class DateTimeUtilities
    {
        public static DateTime Max(this DateTime left, DateTime right)
        {
            return left >= right ? left : right;
        }

        public static DateTime Min(this DateTime left, DateTime right)
        {
            return left <= right ? left : right;
        }

        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }

        public static DateTime MakeEarly(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime MakeLate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
    }
}