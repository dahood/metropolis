using System.Windows.Forms;

namespace Metropolis.Utilities
{
    public static class DialogUtils
    {
        public static string GetSourceDirectory(string type, string initialDirectory)
        {
            using (var dialog = new FolderBrowserDialog {Description = $"Locate {type} Source Directory", SelectedPath = initialDirectory})
            {
                var result = dialog.ShowDialog();
                return result == DialogResult.OK ? dialog.SelectedPath : initialDirectory;
            }
        }

        public static string GetFileName(string filter, string initialFile = null)
        {
            using (var dialog = new OpenFileDialog {FileName = initialFile, Filter = filter})
            {
                dialog.ShowDialog();
                return dialog.FileName != string.Empty ? dialog.FileName : initialFile;
            }
        }
    }
}