using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class CodeInspectorTabViewModel : MvxViewModel
    {
        public CodeInspectorSummaryViewModel Summary { get; }
        public CodeInspectorSourceCodeViewModel SourceCode { get; }
        public CodeInspectorMembersViewModel Members { get; }
        public CodeInspectorDuplicateViewModel Duplicates { get; }
    }
}
