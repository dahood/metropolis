using System.Windows.Media;
using Metropolis.Api.Domain;

namespace Metropolis.Models
{
    public abstract class AbstractHeatMap
    {
        public static readonly Brush CityBlock = Brushes.Gray;

        public abstract Brush GetBrush(Instance toScore);

        protected static bool MeetsThreshold(double score, int threshold)
        {
            return score - threshold < double.Epsilon;
        }
    }
}