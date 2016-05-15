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

        public static DateTime MaxDateTime => DateTime.MaxValue;
        public static DateTime MinDateTime => DateTime.MinValue;
    }
}
