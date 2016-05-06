using System.Windows.Media;

namespace Metropolis.Models
{
    public class BrushFactory
    {
        public static readonly Brush CityBlock = Brushes.Gray;

        private static readonly Brush VeryBad = Brushes.OrangeRed;
        private static readonly Brush Bad = Brushes.Orange;
        private static readonly Brush AlmostAcceptable = Brushes.Yellow;
        private static readonly Brush Acceptable = Brushes.LightGreen;
        private static readonly Brush Good = Brushes.Green;


        public static Brush GetBrushForToxicity(double score)
        {
            if (score < double.Epsilon)
                return Good;
            if (score - 1 < double.Epsilon)
                return Acceptable;
            if (score - 2 < double.Epsilon)
                return AlmostAcceptable;
            if (score - 5 < double.Epsilon)
                return Bad;
            return VeryBad;
        }
    }
}