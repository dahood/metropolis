using System.Windows;
using System.Windows.Controls;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MembersViewPanel.xaml
    /// </summary>
    public partial class MembersViewPanel : UserControl
    {
        private readonly UserControl[] memberViews;

        public MembersViewPanel()
        {
            InitializeComponent();
            Loaded += ControlLoaded;
            memberViews = new UserControl[] { CSharpMembersView, ECMAMembersView, JavaMembersView };
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as CodeInspectorViewModel;
            if (context == null) return;

            memberViews.ForEach(each => each.Visibility = Visibility.Collapsed);
            switch (context.SourceType)
            {
                case RepositorySourceType.CSharp:
                    CSharpMembersView.Visibility = Visibility.Visible;
                    break;
                case RepositorySourceType.Java:
                    JavaMembersView.Visibility = Visibility.Visible;
                    break;
                default:
                    ECMAMembersView.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
