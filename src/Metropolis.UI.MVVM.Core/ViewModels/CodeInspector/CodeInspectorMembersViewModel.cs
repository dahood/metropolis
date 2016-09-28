using System.Collections.Generic;
using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class CodeInspectorMembersViewModel : MvxViewModel
    {
        private List<MemberDetail> members = new List<MemberDetail>();
        private SourceTypeDetail sourceTypeDetail;

        public List<MemberDetail> Members
        {
            get { return members; }
            set { SetProperty(ref members, value); }
        }

        public SourceTypeDetail SourceTypeDetail
        {
            get { return sourceTypeDetail; }
            set { SetProperty(ref sourceTypeDetail, value); }
        }
    }   
}