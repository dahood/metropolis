using System.Windows;

namespace Metropolis.Views
{
    /// <summary>
    ///     Set user preferences like where is FxCop, MSBuild, etc.
    /// </summary>
    public partial class UserPreferences
    {
        public UserPreferences()
        {
            InitializeComponent();
        }

        private static IWorkspaceProvider WorkSpaceProvider => App.WorkspaceProvider;

        private void SelectFxCopLocation(object sender, RoutedEventArgs e)
        {
            //WorkSpaceProvider. = DialogUtils.GetFileName("exe", FxCopBinaryLocationTextBox.Text);
        }
    }
}