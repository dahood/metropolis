using System;

namespace Metropolis.Api.Utilities
{
    public class Clock
    {
        private static DateTime? _frozenTime;

        public static void Freeze()
        {
            _frozenTime = DateTime.Now;
        }

        public static void Freeze(DateTime date)
        {
            _frozenTime = date;
        }

        public static void Thaw()
        {
            _frozenTime = null;
        }

        public static DateTime Now => _frozenTime ?? DateTime.Now;


        public static DateTime EndOfToday => DateTimeUtilities.MakeLate(Now);

        public static DateTime Today => DateTimeUtilities.MakeEarly(Now);
    }
    public static class DateTimeUtilities
    {
        public static DateTime Max(DateTime left, DateTime right)
        {
            return left >= right ? left : right;
        }

        public static DateTime Min(DateTime left, DateTime right)
        {
            return left <= right ? left : right;
        }

        public static DateTime StartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime EndOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }

        public static DateTime MakeEarly(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime MakeLate(DateTime date)
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
