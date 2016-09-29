namespace Metropolis.UI.MVVM.Core.Models
{
    public class DuplicateModel
    {
        public int LinesOfCode { get; private set; }
        public int LineNumber { get; private set; }
        public string Location { get; private set; }
        public DuplicateModel[] CopyCats { get; private set; }
    }
}