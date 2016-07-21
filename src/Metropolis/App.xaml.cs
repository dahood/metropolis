using System;
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

            AppDomain.CurrentDomain.UnhandledException += UniversalErrorHandle;
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
        static void UniversalErrorHandle(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
            Logger.Fatal(e);
            Logger.Fatal($"Runtime terminating: {args.IsTerminating}");
        }
    }
}