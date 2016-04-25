using System.Windows.Media.Media3D;

namespace Metropolis
{
    public interface IHighlightModel
    {
        void Reset();
        IHighlightModel Swap(GeometryModel3D model);
    }
}