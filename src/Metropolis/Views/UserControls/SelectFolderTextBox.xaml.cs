using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Metropolis.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SelectFolderTextBox.xaml
    /// </summary>
    public partial class SelectFolderTextBox : TextBox, IPauseEvents
    {
        public Popup Popup => Template.FindName("PART_Popup", this) as Popup;
        public ListBox ItemList => Template.FindName("PART_ItemList", this) as ListBox;
        public ScrollViewer Host => Template.FindName("PART_ContentHost", this) as ScrollViewer;
        public UIElement TextBoxView => (from object o in LogicalTreeHelper.GetChildren(Host) select o as UIElement).FirstOrDefault();

        private bool loaded = false;
        private string lastPath;
        private bool eventsSuspended = false; //allows better interaction between textbox and list box control
        private readonly Key[] supportedItemListKeys = {Key.Enter, Key.Tab, Key.Oem5}; //supported keys

        public SelectFolderTextBox()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            loaded = true;
            ItemList.SelectionMode = SelectionMode.Single;
            KeyDown += AutoCompleteTextBox_KeyDown;
            PreviewKeyDown += AutoCompleteTextBox_PreviewKeyDown;            
            ItemList.PreviewMouseDown += ItemList_PreviewMouseDown;
            ItemList.KeyDown += ItemList_KeyDown;
            ItemList.SelectionChanged += ItemList_SelectionChanged;
            
            AddHandler(GotFocusEvent, (RoutedEventHandler)delegate { SelectAll(); });          
        }

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (new SuspendEvents(this))
            {
                var list = sender as ListBox;
                if (list == null || list.SelectedIndex == -1) return;

                Text = list.SelectedValue as string; //set value
                CaretIndex = Text.Length; //position cursor
            }
        }

        private void AutoCompleteTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (eventsSuspended)
            {
                e.Handled = true;
                return;
            }

            if (e.Key != Key.Down || ItemList.Items.Count <= 0 || e.OriginalSource is ListBoxItem) return;

            ItemList.Focus();
            ItemList.SelectedIndex = 0;
            var lbi = ItemList.ItemContainerGenerator.ContainerFromIndex(ItemList.SelectedIndex) as ListBoxItem;
            if (lbi == null) e.Handled = true;
            else lbi.Focus();
            e.Handled = true;
        }

        private void ItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.OriginalSource is ListBoxItem) || eventsSuspended) return;

            var tb = (ListBoxItem) e.OriginalSource;
            Text = (tb.Content as string);
            
            if (!supportedItemListKeys.Any(x => x == e.Key)) return;

            Popup.IsOpen = false;
            UpdateSource();
            if (e.Key == Key.Tab) e.Handled = true;
        }
        
        private void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || eventsSuspended) return;
            Popup.IsOpen = false;
            UpdateSource();
        }

        private void UpdateSource()
        {
            using (new SuspendEvents(this))
            {
                if (GetBindingExpression(TextProperty) == null)
                    GetBindingExpression(TextProperty).UpdateSource();

                Focus(); //Textbox to regain focus
                CaretIndex = Text.Length; //position cursor
            }
        }

        private void ItemList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || eventsSuspended) return;
            var tb = e.OriginalSource as TextBlock;
            if (tb == null) return;

            Text = tb.Text;
            UpdateSource();
            Popup.IsOpen = false;
            e.Handled = true;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!loaded || eventsSuspended) return;
            try
            {
                if (lastPath != Path.GetDirectoryName(Text))
                {
                    lastPath = Path.GetDirectoryName(Text);
                    var paths = Lookup(Text);

                    ItemList.Items.Clear();
                    foreach (var path in paths.Where(path => !(string.Equals(path, Text, StringComparison.CurrentCultureIgnoreCase))))
                        ItemList.Items.Add(path);
                }                                                   

                Popup.IsOpen = true;

                ItemList.Items.Filter = p =>
                {
                    var path = p as string;
                    return path != null &&
                           path.StartsWith(Text, StringComparison.CurrentCultureIgnoreCase) &&
                           !(string.Equals(path, Text, StringComparison.CurrentCultureIgnoreCase));
                };
            }
            catch
            {
            }
        }
        
        private static IEnumerable<string> Lookup(string path)
        {
            try
            {
                var thePath = Path.GetDirectoryName(path);
                if (thePath == null || !Directory.Exists(thePath)) return new string[0];

                return (from di in new DirectoryInfo(thePath).GetDirectories()
                        where di.FullName.StartsWith(path, StringComparison.CurrentCultureIgnoreCase)
                        select di.FullName).ToArray();
            }
            catch (Exception)
            {
            }
            return new string[0];                
        }

        public void PauseEvents()
        {
            eventsSuspended = true;
        }

        public void EnableEvents()
        {
            eventsSuspended = false;
        }
    }

    public interface IPauseEvents
    {
        void PauseEvents();
        void EnableEvents();
    }

    public class SuspendEvents : IDisposable
    {
        private readonly IPauseEvents _target;

        public SuspendEvents(IPauseEvents target)
        {
            _target = target;
            _target.PauseEvents();
        }

        public void Dispose()
        {
            _target.EnableEvents();
        }
        
    }

}
