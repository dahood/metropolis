using Metropolis.UI.MVVM.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace Metropolis.UI.MVVM.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainViewModel>();
        }
    }
}
