using System.Windows.Controls;
using System.Windows.Media;

namespace Metropolis.Views.UserControls
{
    /// <summary>
    ///  Provides a summary of code metrics for print or display
    /// </summary>
    public partial class CodeMetricsSummary
    {
        public CodeMetricsSummary()
        {
            InitializeComponent();
            //Foreground = Brushes.White;
        }

        private void Project_Info_Changed(object sender, TextChangedEventArgs e)
        {
            //TODO: workspaceProvider.Workspace.Name = ProjectNameTextBox.Text;
        }
    }
}