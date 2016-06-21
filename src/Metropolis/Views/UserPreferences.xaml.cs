using System.Windows;
using Metropolis.Api.IO;
using Metropolis.Utilities;

namespace Metropolis.Views
{
    /// <summary>
    ///     Set user preferences like where is FxCop, MSBuild, etc.
    /// </summary>
    public partial class UserPreferences
    {
        private readonly IUserPreferences preferences;
        public UserPreferences()
        {
            InitializeComponent();
            preferences = new Api.IO.UserPreferences();
            FxCopBinaryLocationTextBox.Text = preferences.FxCopPath;
            MsBuildLocationTextBox.Text = preferences.MsBuildPath;
        }

        private void SelectFxCopLocation(object sender, RoutedEventArgs e)
        {
            preferences.FxCopPath = DialogUtils.GetFileName(@"Fx Cop Executable (.exe)|*.exe;", FxCopBinaryLocationTextBox.Text);
        }

        private void SelectMsBuildLocation(object sender, RoutedEventArgs e)
        {
            preferences.MsBuildPath = DialogUtils.GetFileName(@"MsBuild Executable (.exe)|*.exe;", MsBuildLocationTextBox.Text);
        }

        private void ClosePreferences(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}