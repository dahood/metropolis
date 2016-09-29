using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.Menu
{
    public class HelpMenuViewModel : MvxViewModel
    {
        public MvxCommand CameraHelp => new MvxCommand(delegate
        {
            /* load camera help tip of day */
        });

        public MvxCommand UserPreferences => new MvxCommand(delegate
        {
            /* change paths for MSBUILD stuff help tip of day */
        });

        public MvxCommand AboutUs => new MvxCommand(delegate
        {
            /* copyright notice, perhaps link to github */
        });
    }
}