using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.MetroBot
{
    public class ProjectRenameViewModel : MvxViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
    }
}