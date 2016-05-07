using System.Windows.Controls;
using System.Windows.Input;
using Metropolis.Api.Core.Domain;
using Metropolis.Layout;

namespace Metropolis
{
    public interface IDisplayClassInformation
    {
        Viewport3D ViewPort { get; }
        AbstractLayout Layout { get; }
        RepositorySourceType SourceType { get; }
        void SetClassInformation(string text);
        event MouseButtonEventHandler MouseRightButtonDown;
    }
}