using System.Windows.Controls;
using System.Windows.Input;
using Metropolis.Common.Models;
using Metropolis.Layout;

namespace Metropolis
{
    public interface IDisplayInstanceInformation
    {
        Viewport3D ViewPort { get; }
        AbstractLayout Layout { get; }
        RepositorySourceType SourceType { get; }
        event MouseButtonEventHandler MouseRightButtonDown;
        void ShowCodeInspector();
    }
}