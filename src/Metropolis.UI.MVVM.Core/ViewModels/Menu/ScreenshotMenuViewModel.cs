using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.Menu
{
    public class ScreenshotMenuViewModel : MvxViewModel
    {
        public MvxCommand TakeScreenshot => new MvxCommand(delegate
        {
            /* call API to run metrics based on previous settings */
        });

        public MvxCommand TakeScreenshotWithSummary => new MvxCommand(delegate
        {
            /* call API to run metrics based on previous settings */
        });
    }
}