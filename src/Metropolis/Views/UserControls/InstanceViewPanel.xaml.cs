using System.Windows;
using System.Windows.Controls;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InstanceViewPanel.xaml
    /// </summary>
    public partial class InstanceViewPanel : UserControl
    {
        private readonly UserControl[] instanceViewers;
        public InstanceViewPanel()
        {
            InitializeComponent();
            Loaded += ControlLoaded;
            instanceViewers = new UserControl[] {CSharpViewer, JavaScriptViewer, JavaViewer};
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as CodeInspectorViewModel;
            if (context == null) return;
            
            instanceViewers.ForEach(each => each.Visibility = Visibility.Collapsed);
            switch (context.SourceType)
            {
                case RepositorySourceType.CSharp:
                    CSharpViewer.Visibility = Visibility.Visible;
                    break;
                case RepositorySourceType.Java:
                    JavaViewer.Visibility = Visibility.Visible;
                    break;
                default:
                    JavaScriptViewer.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
