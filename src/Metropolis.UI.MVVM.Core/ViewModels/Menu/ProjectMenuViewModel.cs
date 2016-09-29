using Metropolis.UI.MVVM.Core.ViewModels.MetroBot;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.Menu
{
    public class ProjectMenuViewModel : MvxViewModel
    {
        public MvxCommand OpenRenameProject => new MvxCommand(delegate { ShowViewModel<ProjectRenameViewModel>(); });
    }
}