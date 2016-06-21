using System.Windows.Media;
using Metropolis.Api.Domain;

namespace Metropolis.Models
{
    public class ToxicityHeatMap : AbstractHeatMap
    {
        /// <summary>
        /// Toxicity Brushes
        /// </summary>
        private static readonly Brush VeryBad = Brushes.OrangeRed;
        private static readonly Brush Bad = Brushes.Orange;
        private static readonly Brush AlmostAcceptable = Brushes.Yellow;
        private static readonly Brush Acceptable = Brushes.LightGreen;
        private static readonly Brush Good = Brushes.Green;

        public override Brush GetBrush(Instance toScore)
        {
            var score = toScore.Toxicity;
            if (MeetsThreshold(score, 0))
                return Good;
            if (MeetsThreshold(score, 1))
                return Acceptable;
            if (MeetsThreshold(score, 2))
                return AlmostAcceptable;
            if (MeetsThreshold(score, 5))
                return Bad;
            return VeryBad;
        }

    }
}