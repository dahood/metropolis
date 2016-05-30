using System.Windows;
using System.Windows.Controls;

namespace Metropolis.Views
{
    /// <summary>
    /// Project Properties like... Name, Source Code Type, Directory of checked out code, etc...
    /// </summary>
    public partial class ProjectProperties
    {
        public ProjectProperties()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ModifyProjectProperties(object sender, TextChangedEventArgs e)
        {
            App.ViewModel.ProjectName = ProjectTextBox.Text;
        }
    }
}