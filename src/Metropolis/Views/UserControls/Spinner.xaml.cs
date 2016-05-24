using System;
using System.Windows;
using System.Windows.Threading;

namespace Metropolis.Views.UserControls
{
    /// <summary>
    ///     Simple Spinner to show that the metrobot is working hard to
    ///     build your metrics for you
    /// </summary>
    public partial class Spinner 
    {
        private static readonly Action EmptyDelegate = delegate { };

        public Spinner()
        {
            InitializeComponent();
        }

        internal void Show()
        {
            Visibility = Visibility.Visible;
            Refresh();
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
            Refresh();
        }

        private void Refresh()
        {
            Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}