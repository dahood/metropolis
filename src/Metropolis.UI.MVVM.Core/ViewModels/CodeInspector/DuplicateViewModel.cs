using System.Collections.Generic;
using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class DuplicateViewModel : MvxViewModel
    {
        private List<DuplicateModel> members = new List<DuplicateModel>();

        public List<DuplicateModel> Members
        {
            get { return members; }
            set { SetProperty(ref members, value); }
        }
    }

    
}