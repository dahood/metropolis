using System;
using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            var working = source.ToList();
            if (working.Count == 0) return null;

            List<IEnumerable<T>> result = new List<IEnumerable<T>>(working.Count / batchSize + 1);
            for (int i = 0; i < working.Count; )
            {
                result.Add(working.GetRange(i, Math.Min(batchSize, working.Count - i)));
                i += batchSize;
            }

            return result;
        }
    }
}