using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class CodeInspectorSourceCodeViewModel : MvxViewModel
    {
        private string fileName;
        private SourceTypeDetail sourceType;

        public string FileName
        {
            get { return fileName; }
            set { SetProperty(ref fileName, value); }
        }

        public SourceTypeDetail SourceType
        {
            get { return sourceType; }
            set { SetProperty(ref sourceType, value); }
        }
    }
}