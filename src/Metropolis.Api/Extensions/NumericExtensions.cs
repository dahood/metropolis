using System.Linq;

namespace Metropolis.Api.Extensions
{
    public static class NumericExtensions
    {
        public static int Max(this int left,  params int[] items)
        {
            return new[] {left}.Append(items).Max();
        }
    }
}
