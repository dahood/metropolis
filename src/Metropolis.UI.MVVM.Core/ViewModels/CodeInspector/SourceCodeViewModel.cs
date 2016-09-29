using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class SourceCodeViewModel : MvxViewModel
    {
        private string fileName;
        private SourceTypeModel sourceType;

        public string FileName
        {
            get { return fileName; }
            set { SetProperty(ref fileName, value); }
        }

        public SourceTypeModel SourceType
        {
            get { return sourceType; }
            set { SetProperty(ref sourceType, value); }
        }
    }
}