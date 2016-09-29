using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.Menu
{
    public class LayoutSelectorViewModel : MvxViewModel
    {
        public MvxCommand ChangeLayout => new MvxCommand(delegate
        {
            /* switch layout -> City,Square,GoldenRatio, etc */
        });
    }
}