using System;
using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        public static T[] Append<T>(this T[] left, T[] right)
        {
            var z = new T[left.Length + right.Length];
            left.CopyTo(z, 0);
            right.CopyTo(z, left.Length);
            return z;
        }
    }
}
