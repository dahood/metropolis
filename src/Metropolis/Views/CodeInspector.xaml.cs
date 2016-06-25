using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis.Views
{
    /// <summary>
    /// Interaction logic for CodeInspector.xaml
    /// </summary>
    public partial class CodeInspector : Window
    {
        public CodeInspector()
        {
            InitializeComponent();
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static void ShowContent(CodeInspectorViewModel viewModel)
        {
            new CodeInspector().Load(viewModel);
        }

        public void Load(CodeInspectorViewModel data)
        {
            DataContext = data;
            Editor.Text = data.FileContents.Data;
            SetHighlighting(data);
            ShowDialog();
        }

        private void SetHighlighting(CodeInspectorViewModel data)
        {
            using (var s = GetHightlightResourceStream(data.SourceType))
            {
                using (var reader = new XmlTextReader(s))
                {
                    Editor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }

        private static Stream GetHightlightResourceStream(RepositorySourceType syntaxHighlighting)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"Metropolis.Resources.Highlighting.{GetHightlight(syntaxHighlighting)}");
        }

        private static string GetHightlight(RepositorySourceType sourceType)
        {
            switch (sourceType)
            {
                case RepositorySourceType.CSharp:
                    return "csharp.xshd";
                case RepositorySourceType.Java:
                    return "java.xshd";
                default:
                    return "javascript.xshd";
            }
        }

        private void InstanceViewer_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
