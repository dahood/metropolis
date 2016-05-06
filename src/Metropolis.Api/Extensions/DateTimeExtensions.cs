using System;

namespace Metropolis.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime TruncateMilliseconds(this DateTime dateTime)
        {
            return dateTime.AddTicks(-(dateTime.Ticks%TimeSpan.TicksPerSecond));
        }

        public static bool EqualsWithin(this DateTime date1, DateTime date2, double withinMillis)
        {
            var result = date1 - date2;

            return (Math.Abs(result.TotalMilliseconds) < withinMillis);
        }
    }
}
