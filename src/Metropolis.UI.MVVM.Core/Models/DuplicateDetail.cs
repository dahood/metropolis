namespace Metropolis.UI.MVVM.Core.Models
{
    public class DuplicateDetail
    {
        public int LinesOfCode { get; private set; }
        public int LineNumber { get; private set; }
        public string Location { get; private set; }
        public DuplicateDetail[] CopyCats { get; private set; }
    }
}