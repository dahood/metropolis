using System.Windows.Forms;
using Metropolis.Api.Domain;
using Metropolis.ViewModels;
using Metropolis.Views;

namespace Metropolis
{
    /// <summary>
    /// Global Application 
    /// </summary>
    public partial class App
    {
        private static ProgressLog _progressLog;
        private static IWorkspaceProvider _workspace;
        private static ProjectDetailsViewModel _viewModel;

        private App()
        {
            if (_progressLog == null)
                _progressLog = new ProgressLog();
            if (_workspace == null)
                _workspace = new WorkspaceProvider();
            if (_viewModel == null)
                _viewModel = new ProjectDetailsViewModel();
        }

        public static CodeBase CodeBase => _workspace.CodeBase;
        public static IWorkspaceProvider WorkspaceProvider => _workspace;

        public static ProjectDetailsViewModel ViewModel => _viewModel;

        public static void ShowLog()
        {
            _progressLog.Activate();
        }

        public static void HideLog()
        {
            _progressLog.Hide();
        }
    }
}