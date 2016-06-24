using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Metropolis.Common.Models;

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

        public static void ShowContent(InstanceInformationFacade highlightedInstance, FileContentsResult data, RepositorySourceType sourceType)
        {
            data.SyntaxHighlighting = sourceType.ToString();
            new CodeInspector { DataContext = data }.Load(data);
        }

        private void Load(FileContentsResult data)
        {
            Editor.Text = data.Data;
            EditorTab.Header = data.FileName;
            SetHighlighting(data);
            ShowDialog();
        }

        private void SetHighlighting(FileContentsResult data)
        {
            using (var s = GetHightlightResourceStream(data.SyntaxHighlighting))
            {
                using (var reader = new XmlTextReader(s))
                {
                    Editor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }

        private static Stream GetHightlightResourceStream(string syntaxHighlighting)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"Metropolis.Resources.Highlighting.{GetHightlight(syntaxHighlighting)}");
        }

        private static string GetHightlight(string syntaxHighlighting)
        {
            switch (syntaxHighlighting)
            {
                case "CSharp":
                    return "csharp.xshd";
                case "Java":
                    return "java.xshd";
                default:
                    return "javascript.xshd";
            }
        }
    }
}
