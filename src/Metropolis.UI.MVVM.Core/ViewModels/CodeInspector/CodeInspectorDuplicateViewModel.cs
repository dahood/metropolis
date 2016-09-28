using System.Collections.Generic;
using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class CodeInspectorDuplicateViewModel : MvxViewModel
    {
        private List<DuplicateDetail> members = new List<DuplicateDetail>();

        public List<DuplicateDetail> Members
        {
            get { return members; }
            set { SetProperty(ref members, value); }
        }
    }

    
}