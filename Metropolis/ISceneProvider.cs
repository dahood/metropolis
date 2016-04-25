using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Metropolis
{
    public interface ISceneProvider
    {
        Viewport3D ViewPort { get; }
        Model3DGroup Model { get; }
        PerspectiveCamera GetCamera();
        event MouseButtonEventHandler MouseLeftButtonDown;
        event MouseButtonEventHandler MouseLeftButtonUp;
        event MouseEventHandler MouseMove;
    }
}