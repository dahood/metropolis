using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.Menu
{
    public class HeatmapSelectorViewModel : MvxViewModel
    {
        public MvxCommand ChangeHeatmap => new MvxCommand(delegate
        {
            /* switch heatmap -> Toxicity, Duplicates, etc */
        });
    }
}