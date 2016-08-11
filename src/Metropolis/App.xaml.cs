using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Metropolis.Api.Domain;
using Metropolis.ViewModels;
using Metropolis.Views;
using NLog;

namespace Metropolis
{
    /// <summary>
    /// Global Application 
    /// </summary>
    public partial class App
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ProgressLog _progressLog;

        private App()
        {
            if (_progressLog == null)
                _progressLog = new ProgressLog();
            if (WorkspaceProvider == null)
                WorkspaceProvider = new WorkspaceProvider();
            if (ViewModel == null)
                ViewModel = new ProjectDetailsViewModel();

            Dispatcher.UnhandledException += GlobalErrorHandle;
        }



        public static CodeBase CodeBase => WorkspaceProvider.CodeBase;
        public static IWorkspaceProvider WorkspaceProvider { get; set; }

        public static ProjectDetailsViewModel ViewModel { get; set; }

        public static void ShowLog()
        {
            _progressLog.Activate();
        }

        public static void HideLog()
        {
            _progressLog.Hide();
        }

        private static void GlobalErrorHandle(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.Exception);

            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MessageBox.Show(e.Exception.Message + Environment.NewLine + 
                "After you select okay we will open the folder with log files using path: " + Environment.NewLine +
                directoryName, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (directoryName != null)
                Process.Start("explorer.exe", Path.Combine(directoryName, "logs"));

            e.Handled = true;
        }
    }
}