using System;
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

            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }



        public static CodeBase CodeBase => WorkspaceProvider.CodeBase;
        public static IWorkspaceProvider WorkspaceProvider { get; private set; }

        public static ProjectDetailsViewModel ViewModel { get; private set; }

        public static void ShowLog()
        {
            _progressLog.Activate();
        }

        public static void HideLog()
        {
            _progressLog.Hide();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.Exception);
            MessageBox.Show(e.Exception.Message + "Please check logfile in " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}