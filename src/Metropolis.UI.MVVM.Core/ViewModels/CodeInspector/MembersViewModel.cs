using System.Collections.Generic;
using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class MembersViewModel : MvxViewModel
    {
        private List<MemberModel> members = new List<MemberModel>();
        private SourceTypeModel sourceTypeModel;

        public List<MemberModel> Members
        {
            get { return members; }
            set { SetProperty(ref members, value); }
        }

        public SourceTypeModel SourceTypeModel
        {
            get { return sourceTypeModel; }
            set { SetProperty(ref sourceTypeModel, value); }
        }
    }   
}