namespace Metropolis.UI.MVVM.Core.Models
{
    public class MemberDetail
    {
        public string Name { get; set; }
        public int LinesOfCode { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int ClassCoupling { get; set; }
    }
}