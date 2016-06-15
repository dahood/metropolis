using System.Windows.Media;

namespace Metropolis.Models
{
    public class HeatMapBrushFactory
    {
        public static readonly Brush CityBlock = Brushes.Gray;

        /// <summary>
        /// Toxicity Brushes
        /// </summary>
        private static readonly Brush VeryBad = Brushes.OrangeRed;
        private static readonly Brush Bad = Brushes.Orange;
        private static readonly Brush AlmostAcceptable = Brushes.Yellow;
        private static readonly Brush Acceptable = Brushes.LightGreen;
        private static readonly Brush Good = Brushes.Green;

        /// <summary>
        /// Code Duplicate Brushes
        /// </summary>
        private static readonly Brush NoDuplicates = new SolidColorBrush(Color.FromRgb(224, 236, 244));
        private static readonly Brush MediumDuplicates = new SolidColorBrush(Color.FromRgb(158, 188, 218));
        private static readonly Brush HighDuplicates = new SolidColorBrush(Color.FromRgb(136, 86, 167));

        public static Brush Toxicity(double score)
        {
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

        public static Brush CodeDuplicates(double score)
        {
            if (MeetsThreshold(score, 0))
                return NoDuplicates;
            if (MeetsThreshold(score, 1))
                return MediumDuplicates;
            if (MeetsThreshold(score, 2))
                return HighDuplicates;

            return HighDuplicates;
        }

        private static bool MeetsThreshold(double score, int threshold)
        {
            return score - threshold < double.Epsilon;
        }
    }
}