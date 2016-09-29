using Metropolis.UI.MVVM.Core.ViewModels.MetroBot;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.Menu
{
    public class CollectionMenuViewModel : MvxViewModel
    {
        public MvxCommand OpenCreateProject => new MvxCommand(delegate { ShowViewModel<ProjectCreationViewModel>(); });
        public MvxCommand RunMetrics => new MvxCommand(delegate { /* call API to run metrics based on previous settings */ });
    }
}