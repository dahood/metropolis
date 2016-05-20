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

        public static bool AreYearMonthDayEqual(DateTime left, DateTime right)
        {
            return left.Year == right.Year && left.Month == right.Month && left.Day == right.Day;
        }

        public static DateTime MakeWithNoMilliseconds(DateTime dateTime)
        {
            return
                new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                    dateTime.Second);
        }

        public static bool CloseEnough(DateTime left, DateTime right)
        {
            return AreYearMonthDayEqual(left, right) &&
                   AreTimeEqual(left, right);
        }
        
        public static bool AreTimeEqual(DateTime left, DateTime right)
        {
            return left.Hour == right.Hour && left.Minute == right.Minute && left.Second == right.Second;
        }

        public static DateTime AsDateTime(TimeSpan time)
        {
            DateTime today = Clock.Today;
            return new DateTime(today.Year, today.Month, today.Day, time.Hours, time.Minutes, 0);
        }

        public static TimeSpan AsTimeSpan(DateTime value)
        {
            return new TimeSpan(0, value.Hour, value.Minute, 0);
        }

        public static bool CloseEnough(DateTime? left, DateTime? right)
        {
            return CloseEnough(left ?? Clock.Today, right ?? Clock.Today);
        }
    }
}