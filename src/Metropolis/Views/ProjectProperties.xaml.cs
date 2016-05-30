using System.Windows;

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
    }
}