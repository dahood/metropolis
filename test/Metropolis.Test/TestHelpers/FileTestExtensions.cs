using System.IO;
namespace Metropolis.Test.TestHelpers
{
    public static class FileTestExtensions
    {
        public static void RemoveFileIfExists(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName) | !File.Exists(fileName)) return;

            File.Delete(fileName);
        }
    }
}
