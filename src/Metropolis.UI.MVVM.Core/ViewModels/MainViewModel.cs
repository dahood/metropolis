using Metropolis.UI.MVVM.Core.ViewModels.Menu;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private string hello;

        public string Hello
        {
            get { return hello; }
            set { SetProperty(ref hello, value); }
        }

        private ViewPort3DViewModel ViewPort;
        private CodeSummaryViewModel codeSummary;
        private ProjectMenuViewModel projectMenu;

    }
}
