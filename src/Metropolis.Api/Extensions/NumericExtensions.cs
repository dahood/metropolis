using System;
using System.Collections;
using System.Linq;

namespace Metropolis.Api.Extensions
{
    public static class NumericExtensions
    {
        public static int Max(this int left,  params int[] items)
        {
            return new[] {left}.Append(items).Max();
        }

        public static void ForEach(this int limit, Action<int> action)
        {
            if (limit <= 0) return;

            for (var i = 0; i < limit; i++)
            {
                action(i);
            }
        }
    }
}
