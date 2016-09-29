using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class TabViewModel : MvxViewModel
    {
        public SummaryViewModel Summary { get; }
        public SourceCodeViewModel SourceCode { get; }
        public MembersViewModel Members { get; }
        public DuplicateViewModel Duplicates { get; }
    }
}
