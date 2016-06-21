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
        private static readonly Brush NoDuplicates = new SolidColorBrush(Color.FromRgb(224, 236, 244));
        private static readonly Brush MediumDuplicates = new SolidColorBrush(Color.FromRgb(158, 188, 218));
        private static readonly Brush HighDuplicates = new SolidColorBrush(Color.FromRgb(136, 86, 167));


        public override Brush GetBrush(Instance toScore)
        {
            var score = toScore.DuplicatePercentage;
            if (MeetsThreshold(score, 0))
                return NoDuplicates;
            if (MeetsThreshold(score, 1))
                return MediumDuplicates;
            if (MeetsThreshold(score, 2))
                return HighDuplicates;

            return HighDuplicates;
        }
    }
}
