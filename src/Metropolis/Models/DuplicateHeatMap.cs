using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Metropolis.Api.Domain;

namespace Metropolis.Models
{
    public class DuplicateHeatMap : AbstractHeatMap
    {
        /// <summary>
        /// Code Duplicate Brushes
        /// </summary>
        private static readonly Brush NoDuplicates = new SolidColorBrush(Color.FromRgb(237, 248, 251));
        private static readonly Brush LowDuplicates = new SolidColorBrush(Color.FromRgb(179, 205, 227));
        private static readonly Brush MediumDuplicates = new SolidColorBrush(Color.FromRgb(140, 150, 198));
        private static readonly Brush HighDuplicates = new SolidColorBrush(Color.FromRgb(136, 86, 167));
        private static readonly Brush VeryHighDuplicates = new SolidColorBrush(Color.FromRgb(129, 15, 124));


        public override Brush GetBrush(Instance toScore)
        {
            var score = toScore.DuplicatePercentage;
            if (MeetsThreshold(score, 0))
                return NoDuplicates;
            if (MeetsThreshold(score, 0.01d))
                return LowDuplicates;
            if (MeetsThreshold(score, 0.03d))
                return MediumDuplicates;
            if (MeetsThreshold(score, 0.04d))
                return HighDuplicates;
            if (MeetsThreshold(score, 0.05d))
                return VeryHighDuplicates;

            return HighDuplicates;
        }
    }
}
