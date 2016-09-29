using System;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels
{
    public class ViewPort3DViewModel : MvxViewModel
    {
        private IntPtr frameHanlder;
        //private Urho.Application application - on set, execute Application.Run();

        public IntPtr FrameHanlder
        {
            get { return frameHanlder; }
            set { SetProperty(ref frameHanlder, value); }
        }
    }
}