using System.Windows;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Views;

namespace Metropolis.UI.MVVM.WPF
{
    /// <summary>
    ///     Bootstrap for MMVM Cross
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new Container();
            MainWindow = window;

            DoSetup();

            window.Show();
        }

        private void DoSetup()
        {
            LoadMvxAssemblyResources();

            var presenter = new MvxSimpleWpfViewPresenter(MainWindow);

            var setup = new Setup(Dispatcher, presenter);
            setup.Initialize();

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();
        }

        private void LoadMvxAssemblyResources()
        {
            for (var i = 0;; i++)
            {
                var key = "MvxAssemblyImport" + i;
                var data = TryFindResource(key);
                if (data == null)
                {
                    return;
                }
            }
        }
    }
}